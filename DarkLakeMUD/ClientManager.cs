using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    public class ClientManager
    {
        private TcpClient _client;
        private NetworkStream _networkStream;
        private Int32 _sessionId;

        public ClientManager(TcpClient client)
        {
            _client = client;
            _networkStream = client.GetStream();

            var rand = new Random();
            _sessionId = rand.Next(0, Int32.MaxValue);

            Log.Information("New ClientManager instantiated. Client has IP {ip} and has been given sessionId {sessionId}",
                ((IPEndPoint)_networkStream.Socket.RemoteEndPoint).Address, _sessionId);
        }

        public void Handle()
        {
            var bytes = new Byte[256];
            String data = null;
            int i;

            // Loop to receive all the data sent by the client. 
            while ((i = _networkStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                data = data.ToUpper();

                byte[] msg = Encoding.ASCII.GetBytes(data);

                _networkStream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }
    }
}
