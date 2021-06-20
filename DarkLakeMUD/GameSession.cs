﻿using DarkLakeMUD.Models;
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
    // Holds everything we need to run a session of the game
    class GameSession
    {
        public Character Character;

        private TcpClient _client;
        private NetworkStream _networkStream;
        private Int32 _sessionId;

        public GameSession(TcpClient client)
        {
            _client = client;
            _networkStream = client.GetStream();

            var rand = new Random();
            _sessionId = rand.Next(0, Int32.MaxValue);

            Log.Information("New ClientManager instantiated. Client has IP {ip} and has been given sessionId {sessionId}",
                ((IPEndPoint)_networkStream.Socket.RemoteEndPoint).Address, _sessionId);
        }

        // For testing, with a preassigned playerName
        public GameSession(TcpClient client, string playerName) : this(client)
        {
            Character = new Character() { Name = playerName };
        }

        // Continually read commands from the telnet sesion, handle them, and provide a response
        public void Play()
        {
            var bytes = new Byte[256];
            String command = null;
            int i;

            while ((i = _networkStream.Read(bytes, 0, bytes.Length)) != 0)
                command = GetClientCommand(bytes, i);
        }

        private string GetClientCommand(byte[] bytes, int byteCount)
        {
            var cmd = Encoding.ASCII.GetString(bytes, 0, byteCount).Trim();

            LogWithSessionId(new string[] { "Received command", cmd});

            return cmd;
        }

        public void SendMessageToClient(string message)
        {
            var msg = Encoding.ASCII.GetBytes(message);
            _networkStream.Write(msg, 0, msg.Length);

            LogWithSessionId(new string[] { "Sent reply", message });
        }

        private void LogWithSessionId(string[] msgs)
        {
            Log.Information("[Session {sessionId}] {msg}", _sessionId, string.Join(" ", msgs));
        }
    }
}
