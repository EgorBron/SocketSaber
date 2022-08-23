# SocketSaber
Library and driver for it that allows you to open localhost TCP socket with lots of your current BS activity.

Also works ingame withount sockets.

## Installation
Download latest release from [here](https://github.com/EgorBron/SocketSaber/releases/latest). 

Then just put `SocketSaberLib.dll` to `Libs` folder in Beat Saber directory, and `SocketSaberMod.dll` to `Plugins` folder.

## Examples

#### Recive data from game
```cs
// subscribe to all events
SocketSaber.SocketSaber.Events += (SocketSaber.EventModels.BaseEventModel event) => {
  if (event.op == 11) {
    var songData = event.d as SocketSaber.EventModels.SongStartModel;
    // we can get some info
    var songDisplayString = $"{songData.songAuthorName} - {songData.songName} ({songData.songSubName})":
    var mapCreatorInfo = $"Map by {songData.mapAuthor} (BeatSaver id: {songData.mapBeatSaverID}, {songData.mapScoreSaberRanked ? "" : "un"}ranked on ScoreSaber)";
 }
}
```

#### Recive data by connecting to socket
```py
# python example
import socket, json

socksaber = socket.socket()
socksaber.connect(('localhost', 9999))

while 1:
  print(json.loads(socksaber.recv(1024)))
```
