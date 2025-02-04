// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TAdminBroadcast } from '../../types';

public readonly adminBroadcast = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquad: ADMIN COMMAND: Message broadcasted <(.+)> from (.+)/,
  );

  if (matches) {
    readonly data: TAdminBroadcast = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      message: matches[3],
      from: matches[4],
      event: LogsReaderEvents.ADMIN_BROADCAST,
    };

    return data;
  }

  return null;
};
