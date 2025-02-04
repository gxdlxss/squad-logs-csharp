// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerSuicide } from '../../types';

public readonly playerSuicide = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquad: Warning: Suicide (.+)/,
  );

  if (matches) {
    readonly data: TPlayerSuicide = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      name: matches[3],
      event: LogsReaderEvents.PLAYER_SUICIDE,
    };

    return data;
  }

  return null;
};
