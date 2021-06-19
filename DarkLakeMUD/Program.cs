using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    class Program
    {
        private static Int32 _port = 9090;
        private static IPAddress _localAddr = IPAddress.Parse("127.0.0.1");

        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/darklogs.txt", rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            Log.Debug("Logging started");

            TcpListener server = null;
            try
            {
                server = new TcpListener(_localAddr, _port);

                Log.Information("Start listening for client requests on {IP}:{Port}", _localAddr, _port);
                server.Start();

                while (true)
                {
                    var client = server.AcceptTcpClient();

                    var manager = new ClientManager(client);
                    var action = new Action(manager.Handle);

                    Task.Run(action);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
                Log.Information("Stopped listening for for new clients.");
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
