// import (замените на using) EventEmitter from 'events';
// import (замените на using) { adminBroadcast } from './adminBroadcast';
// import (замените на using) { deployableDamaged } from './deployableDamaged';
// import (замените на using) { newGame } from './newGame';
// import (замените на using) { playerConnected } from './playerConnected';
// import (замените на using) { playerDamaged } from './playerDamaged';
// import (замените на using) { playerDied } from './playerDied';
// import (замените на using) { playerDisconnected } from './playerDisconnected';
// import (замените на using) { playerPossess } from './playerPossess';
// import (замените на using) { playerRevived } from './playerRevived';
// import (замените на using) { playerSuicide } from './playerSuicide';
// import (замените на using) { playerUnpossess } from './playerUnpossess';
// import (замените на using) { playerWounded } from './playerWounded';
// import (замените на using) { roundEnded } from './roundEnded';
// import (замените на using) { roundTickets } from './roundTickets';
// import (замените на using) { roundWinner } from './roundWinner';
// import (замените на using) { serverTickRate } from './serverTickRate';
// import (замените на using) { squadCreated } from './squadCreated';
// import (замените на using) { vehicleDamaged } from './vehicleDamaged';
// import (замените на using) { applyExplosiveDamage } from './applyExplosiveDamage';

readonly parsers = [
  adminBroadcast,
  newGame,
  playerConnected,
  playerDisconnected,
  playerRevived,
  playerWounded,
  playerDied,
  playerPossess,
  playerUnpossess,
  playerDamaged,
  playerSuicide,
  deployableDamaged,
  roundEnded,
  roundTickets,
  roundWinner,
  squadCreated,
  vehicleDamaged,
  serverTickRate,
  applyExplosiveDamage
];

public readonly parseLine = (line: string, emitter: EventEmitter) => {
  for (var i = 0; i < parsers.length; i++) {
    readonly result = parsers[i](line);

    if (result) {
      emitter.emit(result.event, result);

      break;
    }
  }
};
