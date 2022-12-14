# SocketSaber <img src="./SocketSaberLogo.svg" width="50" alt="SockS Logo">

Library and mod for it that allows you to open localhost TCP socket with lots of your current BS activity.

Also works ingame withount sockets.

## Installation

Download latest release from [here](https://github.com/EgorBron/SocketSaber/releases/latest).

Then just put `SocketSaber.dll` to `Plugins` folder in Beat Saber directory.

## Examples

#### Recive data from game

```cs
// subscribe to all events
SocketSaber.SockSEventation.EveryEvent += (SocketSaber.EventModels.BaseEM e) => {
  if (e.op == SocketSaber.EventModels.EventList.SongStart) {
    var songData = e.d as SocketSaber.EventModels.SongStartEM;
    // we can get some info
    var songDisplayString = $"{songData.songAuthorName} - {songData.songName} ({songData.songSubName})";
    var mapCreatorInfo = $"Map by {songData.mapAuthor} (BeatSaver id: {songData.mapBeatSaverID}, {songData.mapScoreSaberRanked ? "" : "un"}ranked on ScoreSaber)";
 }
}
// subscribation to certain events also available
SocketSaber.SockSEventation.SongStartEvent += (SocketSaber.EventModels.SongStartEM e) => {
  var songDisplayString = $"{e.songAuthorName} - {e.songName} ({e.songSubName})";
}
```

#### Recive data by connecting to socket

```py
# python example
import socket, json

socksaber = socket.socket()
socksaber.connect(('localhost', 9999))

while 1:
  recv = json.loads(socksaber.recv(1024))
  if recv['op'] == 11: # op 11 - song start
    print(recv['d']['songName']) # will print song name (omg really?)
```
