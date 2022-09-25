using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketSaber.Utils;
using SocketSaber.EventModels;

namespace SocketSaber {
    
    public class SockSEventation : IDisposable {
        TcpListener socket;
        List<TcpClient> tcpClients = new List<TcpClient>();

        public static event BaseEventDelegate EveryEvent;
        public static event SongStartEventDelegate SongStartEvent;
        public static event SongEndEventDelegate SongEndEvent;

        public SockSEventation(TcpListener sock) {
            socket = sock;
        }
        public void StartAccepting() {
            new Thread(() => { while (true) { var client = socket.AcceptTcpClient(); Plugin.Log.Info("New connection"); tcpClients.Add(client); }}).Start();
        }
        public void SendRawDataToAll(string data) {
            foreach (var client in tcpClients) {
                var stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
                // todo: send to event subscribers from mods
            }
        }
        public void Dispose() {
            foreach (var client in tcpClients) client.Close();
            tcpClients.Clear();
            socket.Stop();
        }
        public void SendDataToAll(BaseEM emData) {
            Plugin.Log.Warn("Sending data");
            var preparedData = JsonConvert.SerializeObject(emData);
            EveryEvent?.Invoke(emData.Data);
            
            switch (emData.Opcode) {
                case EventList.MenuLoad: break;
                case EventList.SongStart:
                case EventList.SongMultiplayerStart:
                case EventList.SongCampaignStart:
                    SongStartEvent?.Invoke((SongStartEM)emData.Data);
                    break;
                case EventList.SongEnd:
                case EventList.SongMultiplayerEnd:
                case EventList.SongCampaignEnd:
                    SongEndEvent?.Invoke((SongEndEM)emData.Data);
                    break;
            }
            SendRawDataToAll(preparedData);
            
        }
    }
}
