using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using SocketSaber.Utils;
using SocketSaber.EventModels;

namespace SocketSaber {
    public class SockSConnectionsProcessor : IDisposable {
        TcpListener socket;
        List<TcpClient> tcpClients = new List<TcpClient>();
        List<Func<BaseEventModel, bool>> subscribers = new List<Func<BaseEventModel, bool>>();
        public SockSConnectionsProcessor(TcpListener sock) {
            socket = sock;
        }
        public event Func<BaseEventModel, bool> Events {
            add {
                
                subscribers.Add(value);
            }
            remove {
                subscribers.Remove(value);
            }
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
        public void SendDataToAll(BaseEventModel data) {
            var preparedData = JsonConvert.SerializeObject(data);
            new Thread(() => {
                foreach (var subscriber in subscribers) {
                    subscriber.Invoke(data);
                }
            }).Start();
            SendRawDataToAll(preparedData);
        }
    }
}
