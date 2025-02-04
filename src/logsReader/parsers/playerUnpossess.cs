// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerUnpossess } from '../../types';

public readonly playerUnpossess = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadTrace: \[DedicatedServer](?:ASQPlayerController::)?OnUnPossess\(\): PC=(.+) \(Online IDs: EOS: ([\w\d]{32}) steam: (\d{17})\)/,
  );

  if (matches) {
    readonly data: TPlayerUnpossess = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      name: matches[3],
      eosID: matches[4],
      steamID: matches[5],
      event: LogsReaderEvents.PLAYER_UNPOSSESS,
    };

    return data;
  }

  return null;
};
