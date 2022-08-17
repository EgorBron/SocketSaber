using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketSaber {
    public class SockSConnectionsProcessor : IDisposable {
        TcpListener socket;
        List<TcpClient> tcpClients = new List<TcpClient>();
        public SockSConnectionsProcessor(TcpListener sock) {
            socket = sock;
        }
        public void StartAccepting() {
            new Thread(() => { while (true) { var client = socket.AcceptTcpClient(); Plugin.Log.Info("New connection"); tcpClients.Add(client); }}).Start();
        }
        public void SendToAll(string data) {
            foreach (var client in tcpClients) {
                var stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        public void Dispose() {
            foreach (var client in tcpClients) client.Close();
            tcpClients.Clear();
            socket.Stop();
        }
    }
}
