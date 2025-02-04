// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TTickRate } from '../../types';

public readonly serverTickRate = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquad: USQGameState: Server Tick Rate: ([0-9.]+)/,
  );

  if (matches) {
    readonly data: TTickRate = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      tickRate: parseFloat(matches[3]),
      event: LogsReaderEvents.TICK_RATE,
    };

    return data;
  }

  return null;
};
