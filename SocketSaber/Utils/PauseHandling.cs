using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SocketSaber.EventModels;
using UnityEngine.UI;

namespace SocketSaber.Utils {

    internal class PauseHandling {
            static PauseHandling() {
            var pause = new GamePause();
            pause.didPauseEvent += () => {
                new Thread(() => {
                    Plugin.ConnProc.SendDataToAll(new BaseEM { Opcode = EventList.SongPaused, Data = null });
                }).Start();
            };
            pause.didResumeEvent += () => {
                new Thread(() => {
                    Plugin.ConnProc.SendDataToAll(new BaseEM { Opcode = EventList.SongResumed, Data = null });
                }).Start();
            };
        }
    }
}
