using HarmonyLib;
using IPA;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace SocketSaber {
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin {
        internal static Plugin Instance { get; private set; }

        internal static IPALogger Log { get; private set; }

        public static SockSConnectionsProcessor ConnProc { get; private set; }

        private static readonly Harmony harmony = new Harmony("net.blusutils.socksaber");

        [Init]
        public Plugin(IPALogger logger) {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart() {
            Log.Notice("Preparing SocketSaber...");
            var sw = Stopwatch.StartNew();
            string ip = "127.0.0.1";
            int port = 9999;
            var socket = new TcpListener(System.Net.IPAddress.Parse(ip), port);
            socket.Start();
            Log.Debug($"Socket started on {ip}:{port}");
            ConnProc = new SockSConnectionsProcessor(socket);
            ConnProc.StartAccepting();
            Log.Debug("Started accepting connections");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Debug("All patches applied");
            sw.Stop();
            Log.Notice($"Preparation of SocketSaber took {sw.Elapsed}");
        }

        [OnExit]
        public void OnApplicationQuit() {
            ConnProc.Dispose();
        }

    }
}
