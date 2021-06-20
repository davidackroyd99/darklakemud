using DarkLakeMUD.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    class Program
    {
        private static Int32 _port = 9090;
        private static IPAddress _localAddr = IPAddress.Parse("127.0.0.1");
        private static List<string> _debugPlayerNames = new List<string>() { "Reizeid", "Behmur", "Mao", "Halveg", "Grargor" };

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

                var mediator = new GameSessionMediator();

                while (true)
                {
                    var client = server.AcceptTcpClient();

                    var session = new GameSession(client, _debugPlayerNames[0]);
                    var action = new Action(session.Play);
                    mediator.AddSession(session);

                    var testRoom = new Room();
                    testRoom.Description.Title = "The Top Of The Jetty";
                    testRoom.Description.Body = "You are standing at the top of the jetty.";

                    new RoomManager().AddCharacterToRoom(testRoom, session.Character, mediator);

                    _debugPlayerNames.RemoveAt(0);

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
        }
    }
}
