// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TRoundWinner } from '../../types';

public readonly roundWinner = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadTrace: \[DedicatedServer](?:ASQGameMode::)?DetermineMatchWinner\(\): (.+) won on (.+)/,
  );

  if (matches) {
    readonly data: TRoundWinner = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      winner: matches[3],
      layer: matches[4],
      event: LogsReaderEvents.ROUND_WINNER,
    };

    return data;
  }

  return null;
};
