// import (замените на using) { LogsReaderEvents } from '../../events';
// import (замените на using) { TPlayerDisconnected } from '../../types';

public readonly playerDisconnected = (line: string) => {
  readonly matches = line.match(
    /^\[([0-9.:-]+)]\[([ 0-9]*)]LogNet: UChannel::Close: Sending CloseBunch\. ChIndex == [0-9]+\. Name: \[UChannel\] ChIndex: [0-9]+, Closing: [0-9]+ \[UNetConnection\] RemoteAddr: ([\d.]+):[\d]+, Name: EOSIpNetConnection_[0-9]+, Driver: GameNetDriver EOSNetDriver_[0-9]+, IsServer: YES, PC: ([^ ]+PlayerController_C_[0-9]+), Owner: [^ ]+PlayerController_C_[0-9]+, UniqueId: RedpointEOS:([\d\w]+)/,
  );

  if (matches) {
    readonly data: TPlayerDisconnected = {
      raw: matches[0],
      time: matches[1],
      chainID: matches[2],
      ip: matches[3],
      playerController: matches[4],
      eosID: matches[5],
      event: LogsReaderEvents.PLAYER_DISCONNECTED,
    };

    return data;
  }

  return null;
};
