public type TLogReaderOptions = {
  id: number;
  filePath: string;
  adminsFilePath: string;
  autoReconnect: boolean;
  readType: 'local' | 'remote';
  host?: string;
  username?: string;
  password?: string;
  timeout?: number;
  logEnabled?: boolean;
};

public type TAdminBroadcast = {
  raw: string;
  time: string;
  chainID: string;
  message: string;
  from: string;
  event: string;
};

public type TNewGame = {
  raw: string;
  time: string;
  chainID: string;
  dlc: string;
  mapClassname: string;
  layerClassname: string;
  event: string;
};

public type TPlayerConnected = {
  raw: string;
  time: string;
  chainID: string;
  playerController: string;
  ip: string;
  eosID: string;
  steamID: string;
  event: string;
};

public type TPlayerDied = {
  raw: string;
  time: string;
  woundTime: string;
  chainID: string;
  victimName: string;
  damage: number;
  attackerPlayerController: string;
  attackerEOSID: string;
  attackerSteamID: string;
  weapon: string;
  event: string;
};

public type TPlayerDisconnected = {
  raw: string;
  time: string;
  chainID: string;
  ip: string;
  eosID: string;
  playerController: string;
  event: string;
};

public type TPlayerPossess = {
  raw: string;
  time: string;
  chainID: string;
  name: string;
  eosID: string;
  steamID: string;
  possessClassname: string;
  pawn: string;
  event: string;
};

public type TPlayerRevived = {
  raw: string;
  time: string;
  chainID: string;
  reviverName: string;
  reviverEOSID: string;
  reviverSteamID: string;
  victimName: string;
  victimEOSID: string;
  victimSteamID: string;
  event: string;
};

public type TPlayerUnpossess = {
  raw: string;
  time: string;
  chainID: string;
  name: string;
  eosID: string;
  steamID: string;
  event: string;
};

public type TPlayerWounded = {
  raw: string;
  time: string;
  chainID: string;
  victimName: string;
  damage: number;
  attackerPlayerController: string;
  attackerEOSID: string;
  attackerSteamID: string;
  weapon: string;
  event: string;
};

public type TDeployableDamaged = {
  raw: string;
  time: string;
  chainID: string;
  deployable: string;
  damage: number;
  weapon: string;
  name: string;
  damageType: string;
  healthRemaining: string;
  event: string;
};

public type TApplyExplosiveDamage = {
  raw: string;
  time: string;
  chainID: string;
  name: string;
  deployable: string;
  playerController: string;
  locations: string;
  event: string;
};

public type TPlayerDamaged = {
  raw: string;
  time: string;
  chainID: string;
  victimName: string;
  damage: number;
  attackerName: string;
  attackerEOSID: string;
  attackerSteamID: string;
  attackerController: string;
  weapon: string;
  event: string;
};

public type TPlayerSuicide = {
  raw: string;
  time: string;
  chainID: string;
  name: string;
  event: string;
};

public type TRoundEnded = {
  raw: string;
  time: string;
  chainID: string;
  event: string;
};

public type TRoundTickets = {
  raw: string;
  time: string;
  chainID: string;
  team: string;
  subfaction: string;
  faction: string;
  action: string;
  tickets: string;
  layer: string;
  level: string;
  event: string;
};

public type TRoundWinner = {
  raw: string;
  time: string;
  chainID: string;
  winner: string;
  layer: string;
  event: string;
};

public type TSquadCreated = {
  raw: string;
  time: string;
  chainID: string;
  name: string;
  eosID: string;
  steamID: string;
  squadID: string;
  squadName: string;
  teamName: string;
  event: string;
};

public type TVehicleDamaged = {
  raw: string;
  time: string;
  chainID: string;
  victimVehicle: string;
  damage: number;
  attackerVehicle: string;
  attackerName: string;
  attackerEOSID: string;
  attackerSteamID: string;
  healthRemaining: string;
  event: string;
};

public type TTickRate = {
  raw: string;
  time: string;
  chainID: string;
  tickRate: number;
  event: string;
};
