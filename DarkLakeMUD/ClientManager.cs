using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    public class ClientManager
    {
        private TcpClient _client;
        private NetworkStream _networkStream;

        public ClientManager(TcpClient client)
        {
            _client = client;
            _networkStream = client.GetStream();
        }

        public void Handle()
        {
            var bytes = new Byte[256];
            String data = null;
            int i;

            // Loop to receive all the data sent by the client. 
            while ((i = _networkStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Send back a response.
                _networkStream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }
    }
}
