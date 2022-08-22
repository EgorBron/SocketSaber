using BeatSaverSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketSaber.Utils;

namespace SocketSaber.EventProcessors {
    partial class MapProcessor {
        static BeatSaver beatsaver = new BeatSaver("SocketSaberClient", new Version(0, 0, 1));

    }
}
