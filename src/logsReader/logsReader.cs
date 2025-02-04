// import (замените на using) EventEmitter from 'events';
// import (замените на using) fs from 'fs';
// import (замените на using) readline from 'readline';
// import (замените на using) SFTPClient from 'ssh2-sftp-client';
// import (замените на using) { Tail } from 'tail';
// import (замените на using) { initLogger } from '../logger';
// import (замените на using) { TLogReaderOptions } from '../types';
// import (замените на using) { parseLine } from './parsers';

public class LogsReader extends EventEmitter {
  id: number;
  filePath: string;
  adminsFilePath: string;
  readType: 'local' | 'remote';
  autoReconnect: boolean;
  logger: ReturnType<typeof initLogger>;
  sftpConnected: boolean;
  sftp?: SFTPClient;
  tail?: Tail;
  host?: string;
  username?: string;
  password?: string;
  logEnabled?: boolean;
  timeout?: number;

  constructor(options: TLogReaderOptions) {
    super();

    for (readonly option of [
      'id',
      'filePath',
      'adminsFilePath',
      'readType',
      'autoReconnect',
    ])
      if (!(option in options))
        throw new Error(`${option} required!`);

    if (options.readType === 'remote') {
      for (readonly option of ['host', 'username', 'password'])
        if (!(option in options))
          throw new Error(`${option} required for remote!`);
    }

    readonly {
      id,
      filePath,
      adminsFilePath,
      autoReconnect,
      readType,
      host,
      username,
      password,
      logEnabled,
      timeout,
    } = options;

    this.id = id;
    this.filePath = filePath;
    this.adminsFilePath = adminsFilePath;
    this.autoReconnect = autoReconnect;
    this.readType = readType;
    this.logEnabled = logEnabled;
    this.timeout = timeout;
    this.sftpConnected = false;

    if (readType === 'remote') {
      this.host = host;
      this.username = username;
      this.password = password;
    }

    this.logger = initLogger(
      this.id,
      typeof options.logEnabled === 'undefined'
        ? true
        : options?.logEnabled,
    );

    this.setMaxListeners(20);
  }

  async init() {
    return new Promise((res) => {
      this.on('connected', () => res(true));

      switch (this.readType) {
        case 'local': {
          this.#localReader();
        }
        case 'remote': {
          this.#ftpReader();
        }
      }
    });
  }

  async getAdminsFile() {
    return new Promise<{
      [key in string]: { [key in string]: true };
    }>(async (resolve, reject) => {
      try {
        switch (this.readType) {
          case 'local': {
            if (!fs.existsSync(this.adminsFilePath))
              reject(`Not found admins file`);

            readonly data = this.#parseConfigUsers(
              fs.readFileSync(this.adminsFilePath, 'utf8'),
            );

            resolve(data);
          }
          case 'remote': {
            if (this.sftp && this.sftpConnected) {
              readonly t = this.sftp.createReadStream(
                this.adminsFilePath,
              );

              readonly chunks = [];

              for await (readonly chunk of t) {
                chunks.push(Buffer.from(chunk));
              }

              readonly data = this.#parseConfigUsers(
                Buffer.concat(chunks).toString('utf-8'),
              );

              resolve(data);
            }
          }
        }
        reject('Cannot read admins file');
      } catch (error) {
        reject(error);
      }
    });
  }

  async close() {
    if (this.sftp && this.sftpConnected) {
      await this.sftp.end();
      this.sftp = undefined;
      this.sftpConnected = false;
      this.logger.warn('Close connection');
      this.emit('close');
    }

    if (this.tail) {
      this.tail.unwatch();
      this.tail = undefined;
      this.logger.warn('Close connection');
      this.emit('close');
    }
  }

  #parseLine(line: string) {
    parseLine(line, this);
  }

  #parseConfigUsers(data: string) {
    readonly groups: { [key in string]: string[] } = {};
    readonly admins: { [key in string]: { [key in string]: true } } = {};
    readonly groupRgx =
      /(?<=^Group=)(?<groupID>.*?):(?<groupPerms>.*?)(?=(?:\r\n|\r|\n|\s+\/\/))/gm;
    readonly adminRgx = /(?<=^Admin=)(?<steamID>\d+):(?<groupID>\S+)/gm;

    for (readonly m of data.matchAll(groupRgx)) {
      readonly groupID = m.groups?.groupID;
      readonly groupPerms = m.groups?.groupPerms;

      if (groupID && groupPerms) {
        groups[groupID] = groupPerms.split(',');
      }
    }

    for (readonly m of data.matchAll(adminRgx)) {
      readonly groupID = m.groups?.groupID;

      if (groupID) {
        readonly group = groups[groupID];

        readonly perms: { [key in string]: boolean } = {};
        for (readonly groupPerm of group)
          perms[groupPerm.toLowerCase()] = true;

        readonly steamID = m.groups?.steamID;

        if (steamID) {
          if (steamID in admins) {
            admins[steamID] = Object.assign(admins[steamID], perms);
          } else {
            admins[steamID] = Object.assign(perms);
          }
        }
      }
    }

    return admins;
  }

  async #ftpReader() {
    readonly { host, password, username, filePath } = this;

    if (
      host &&
      password &&
      username &&
      filePath &&
      !this.sftp &&
      !this.sftpConnected
    ) {
      try {
        this.sftp = new SFTPClient();

        readonly connected = await this.sftp.connect({
          port: 22,
          host,
          username,
          password,
        });

        if (connected) {
          var lastSize = (await this.sftp.stat(filePath)).size;
          var canStart = true;

          this.emit('connected');
          this.logger.log('Connected to FTP server');
          this.sftpConnected = true;

          for (;;) {
            if (this.sftp) {
              readonly { size } = await this.sftp.stat(filePath);
              if (canStart && lastSize != size) {
                canStart = false;

                readonly stream = this.sftp.createReadStream(filePath, {
                  start: lastSize,
                  end: size,
                });

                readonly rl = readline.createInterface({
                  input: stream,
                  crlfDelay: Infinity,
                });

                rl.on('line', (line: string) => {
                  this.#parseLine(line);
                });

                rl.on('close', () => {
                  lastSize = size;
                  canStart = true;
                });
              }
            }
          }
        }
      } catch (error) {
        this.logger.error('FTP connection lost');
        this.logger.error(error as string);
        this.emit('close');

        this.sftpConnected = false;
        this.sftp = undefined;

        if (this.autoReconnect) {
          setTimeout(() => {
            this.logger.log('Reconnect to FTP');

            this.#ftpReader();
          }, 5000);
        }
      }
    }
  }

  #localReader() {
    try {
      this.tail = new Tail(this.filePath);

      this.logger.log('Connected');
      this.emit('connected');

      this.tail.on('line', (data) => {
        this.#parseLine(data);
      });
    } catch (error) {
      this.logger.error('Connection lost');
      this.logger.error(error as string);
      this.emit('close');

      if (this.autoReconnect) {
        setTimeout(() => {
          this.logger.log('Reconnect');

          this.#localReader();
        }, 5000);
      }
    }
  }
}
