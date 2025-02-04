// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerPossess } from '../../types';

public readonly playerPossess = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadTrace: \[DedicatedServer](?:ASQPlayerController::)?OnPossess\(\): PC=(.+) \(Online IDs: EOS: ([\w\d]{32}) steam: (\d{17})\) Pawn=([A-z0-9_]+)_C/,
  );

  if (matches) {
    readonly data: TPlayerPossess = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      name: matches[3],
      eosID: matches[4],
      steamID: matches[5],
      possessClassname: matches[6],
      pawn: matches[5],
      event: LogsReaderEvents.PLAYER_POSSESS,
    };

    return data;
  }

  return null;
};
