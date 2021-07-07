using DarkLakeMUD.Models;
using DarkLakeMUD.Parser;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    // Holds everything we need to run a session of the game
    class GameSession
    {
        public Character Character;

        private TcpClient _client;
        private NetworkStream _networkStream;
        private Int32 _sessionId;
        private RoomManager _roomManager;
        private GameSessionMediator _mediator;
        private IParser _parser;

        public GameSession(TcpClient client, RoomManager roomManager, GameSessionMediator mediator, IParser parser)
        {
            _client = client;
            _networkStream = client.GetStream();
            _roomManager = roomManager;
            _mediator = mediator;
            _parser = parser;

            var rand = new Random();
            _sessionId = rand.Next(0, Int32.MaxValue);

            Log.Information("New ClientManager instantiated. Client has IP {ip} and has been given sessionId {sessionId}",
                ((IPEndPoint)_networkStream.Socket.RemoteEndPoint).Address, _sessionId);

            Task.Run(new Action(PollForDisconnect));
        }

        // For testing, with a preassigned playerName
        public GameSession(TcpClient client, RoomManager roomManager, GameSessionMediator mediator, IParser parser, string playerName) 
            : this(client, roomManager, mediator, parser)
        {
            Character = new Character() { Name = playerName };
        }

        // Continually read commands from the telnet sesion, handle them, and provide a response
        public void Play()
        {
            var bytes = new Byte[256];
            int i;

            _roomManager.AddCharacterToRoom(_roomManager.GetRooms()[0], Character, _mediator);

            while ((i = _networkStream.Read(bytes, 0, bytes.Length)) != 0)
            {
                var command = GetClientCommand(bytes, i);
                var direction = (Direction)Enum.Parse(typeof(Direction), command.Noun, true);

                if (command.Verb == Verb.Go)
                    _roomManager.MoveCharacter(Character, direction, _mediator);
            }
        }

        public void PollForDisconnect()
        {
            if (_networkStream.Socket.Poll(-1, SelectMode.SelectRead))
            {
                LogWithSessionId("session disconnected");
                
                _roomManager.EvictCharacter(Character);
                _mediator.SessionClosed(this);
            }
        }

        private Command GetClientCommand(byte[] bytes, int byteCount)
        {
            var cmd = Encoding.ASCII.GetString(bytes, 0, byteCount).Trim();

            LogWithSessionId(new string[] { "Received command", cmd });
            
            return _parser.Parse(cmd);
        }

        public void SendMessageToClient(string message)
        {
            var msg = Encoding.ASCII.GetBytes(message);
            _networkStream.Write(msg, 0, msg.Length);

            LogWithSessionId(new string[] { "Sent reply", message });
        }

        private void LogWithSessionId(string msg) => LogWithSessionId(new string[] { msg });

        private void LogWithSessionId(string[] msgs) => Log.Information("[Session {sessionId}] {msg}", _sessionId, string.Join(" ", msgs));
    }
}
