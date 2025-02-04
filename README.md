# SquadLogs C#

This library is designed for the game Squad, allowing you to easily parse game logs in C#. It supports reading files locally and via the SFTP protocol. I hope this will be useful to you!

## Installation

Before using, ensure you have [.NET](https://dotnet.microsoft.com/) installed.

You can install the package using NuGet Package Manager:

```console
$ dotnet add package SquadLogs
```

or via Visual Studio Package Manager Console:

```console
PM> Install-Package SquadLogs
```

## Quick Start

### SFTP

```csharp
using SquadLogs;

class Program
{
    static async Task Main(string[] args)
    {
        var logsReader = new LogsReader(
            id: 1,
            autoReconnect: true,
            readType: "remote",
            adminsFilePath: "/SquadGame/ServerConfig/Admins.cfg",
            filePath: "/SquadGame/Saved/Logs/SquadGame.log",
            host: "127.0.0.1",
            username: "root",
            password: "123456"
        );

        await logsReader.Init();

        var admins = await logsReader.GetAdminsFile();

        logsReader.On("PLAYER_CONNECTED", data =>
        {
            Console.WriteLine(data);
        });
    }
}
```

### LOCAL

```csharp
using SquadLogs;

class Program
{
    static async Task Main(string[] args)
    {
        var logsReader = new LogsReader(
            id: 1,
            autoReconnect: true,
            readType: "local",
            adminsFilePath: "/SquadGame/ServerConfig/Admins.cfg",
            filePath: "/SquadGame/Saved/Logs/SquadGame.log"
        );

        await logsReader.Init();

        var admins = await logsReader.GetAdminsFile();

        logsReader.On("PLAYER_CONNECTED", data =>
        {
            Console.WriteLine(data);
        });
    }
}
```

### Events

| Event                   | Return       | Type                  |
| ----------------------- | ------------ | --------------------- |
| **ADMIN_BROADCAST**     | **response** | `AdminBroadcast`     |
| **DEPLOYABLE_DAMAGED**  | **response** | `DeployableDamaged`  |
| **NEW_GAME**            | **response** | `NewGame`            |
| **PLAYER_CONNECTED**    | **response** | `PlayerConnected`    |
| **PLAYER_DISCONNECTED** | **response** | `PlayerDisconnected` |
| **PLAYER_DAMAGED**      | **response** | `PlayerDamaged`      |
| **PLAYER_DIED**         | **response** | `PlayerDied`         |
| **PLAYER_POSSESS**      | **response** | `PlayerPossess`      |
| **PLAYER_UNPOSSESS**    | **response** | `PlayerUnpossess`    |
| **PLAYER_REVIVED**      | **response** | `PlayerRevived`      |
| **PLAYER_SUICIDE**      | **response** | `PlayerSuicide`      |
| **PLAYER_WOUNDED**      | **response** | `PlayerWounded`      |
| **ROUND_WINNER**        | **response** | `RoundWinner`        |
| **ROUND_ENDED**         | **response** | `RoundEnded`         |
| **ROUND_TICKETS**       | **response** | `RoundTickets`       |
| **SQUAD_CREATED**       | **response** | `SquadCreated`       |
| **VEHICLE_DAMAGED**     | **response** | `VehicleDamaged`     |
| **TICK_RATE**           | **response** | `TickRate`           |
| **connected**           | null         | null                  |
| **close**               | null         | null                  |