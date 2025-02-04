// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TDeployableDamaged } from '../../types';

public readonly deployableDamaged = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadTrace: \[DedicatedServer](?:ASQDeployable::)?TakeDamage\(\): ([A-z0-9_]+)_C_[0-9]+: ([0-9.]+) damage attempt by causer ([A-z0-9_]+)_C_[0-9]+ instigator (.+) with damage type ([A-z0-9_]+)_C health remaining ([0-9.]+)/,
  );

  if (matches) {
    readonly data: TDeployableDamaged = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      deployable: matches[3],
      damage: parseFloat(matches[4]),
      weapon: matches[5],
      name: matches[6],
      damageType: matches[7],
      healthRemaining: matches[8],
      event: LogsReaderEvents.DEPLOYABLE_DAMAGED,
    };

    return data;
  }

  return null;
};
