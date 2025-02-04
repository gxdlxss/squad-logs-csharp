// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TRoundEnded } from '../../types';

public readonly roundEnded = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogGameState: Match State Changed from InProgress to WaitingPostMatch/,
  );

  if (matches) {
    readonly data: TRoundEnded = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      event: LogsReaderEvents.ROUND_ENDED,
    };

    return data;
  }

  return null;
};
