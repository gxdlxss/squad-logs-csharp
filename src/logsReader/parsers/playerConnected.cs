// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerConnected } from '../../types';

public readonly playerConnected = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquad: PostLogin: NewPlayer: [^\s]+ .+PersistentLevel\.([^\s]+) \(IP: ([\d.]+) \| Online IDs: EOS: ([0-9a-f]{32}) steam: (\d+)\)/,
  );

  if (matches) {
    readonly data: TPlayerConnected = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      playerController: matches[3],
      ip: matches[4],
      eosID: matches[5],
      steamID: matches[6],
      event: LogsReaderEvents.PLAYER_CONNECTED,
    };

    return data;
  }

  return null;
};
