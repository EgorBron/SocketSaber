using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketSaber.EventModels;

namespace SocketSaber.Utils {

    internal class PauseHandling {
            static PauseHandling() {
            var pause = new GamePause();
            pause.didPauseEvent += () => {
                new Thread(() => {
                    var mainDict = new DictStrO {
                        ["op"] = EventList.SongPaused,
                        ["d"] = null
                    };
                    Plugin.ConnProc.SendRawDataToAll(JsonConvert.SerializeObject(mainDict));
                }).Start();
            };
            pause.didResumeEvent += () => {
                new Thread(() => {
                    var mainDict = new DictStrO {
                        ["op"] = EventList.SongResumed,
                        ["d"] = null
                    };
                    Plugin.ConnProc.SendRawDataToAll(JsonConvert.SerializeObject(mainDict));
                }).Start();
            };
        }
    }
}
