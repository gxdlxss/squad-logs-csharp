// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TRoundTickets } from '../../types';

public readonly roundTickets = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogSquadGameEvents: Display: Team ([0-9]), (.*) \( ?(.*?) ?\) has (won|lost) the match with ([0-9]+) Tickets on layer (.*) \(level (.*)\)!/,
  );

  if (matches) {
    readonly data: TRoundTickets = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      team: matches[3],
      subfaction: matches[4],
      faction: matches[5],
      action: matches[6],
      tickets: matches[7],
      layer: matches[8],
      level: matches[9],
      event: LogsReaderEvents.ROUND_TICKETS,
    };

    return data;
  }

  return null;
};
