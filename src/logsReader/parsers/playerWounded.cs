// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerWounded } from '../../types';

public readonly playerWounded = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadTrace: \[DedicatedServer](?:ASQSoldier::)?Wound\(\): Player:(.+) KillingDamage=(?:-)*([0-9.]+) from ([A-z_0-9]+) \(Online IDs: EOS: ([\w\d]{32}) steam: (\d{17}) \| Controller ID: ([\w\d]+)\) caused by ([A-z_0-9-]+)_C/,
  );

  if (matches) {
    readonly data: TPlayerWounded = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      victimName: matches[3],
      damage: parseFloat(matches[4]),
      attackerPlayerController: matches[5],
      attackerEOSID: matches[6],
      attackerSteamID: matches[7],
      weapon: matches[9],
      event: LogsReaderEvents.PLAYER_WOUNDED,
    };

    return data;
  }

  return null;
};
