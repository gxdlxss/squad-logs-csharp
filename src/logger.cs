// import (замените на using) chalk from 'chalk';
// import (замените на using) { format } from 'date-fns';

readonly getTime = () => format(new Date(), 'd LLL HH:mm:ss');

public readonly initLogger = (id: number, enabled: boolean) => ({
  log: (...text: string[]) => {
    enabled &&
      console.log(
        chalk.yellow(`[SquadLogs][${id}][${getTime()}]`),
        chalk.green(text),
      );
  },

  warn: (...text: string[]) => {
    enabled &&
      console.log(
        chalk.yellow(`[SquadLogs][${id}][${getTime()}]`),
        chalk.magenta(text),
      );
  },

  error: (...text: string[]) => {
    enabled &&
      console.log(
        chalk.yellow(`[SquadLogs][${id}][${getTime()}]`),
        chalk.red(text),
      );
  },
});
