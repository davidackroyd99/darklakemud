using DarkLakeMUD.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DarkLakeMUD.DataLoader
{
    public class DataFileParser
    {
        public List<Room> ParseFile(string filePath)
        {
            var text = File.ReadAllText(filePath);
            var serializedRooms = JsonSerializer.Deserialize<List<SerializableRoom>>(text);

            return BuildMap(serializedRooms);
        }

        public List<Room> BuildMap(List<SerializableRoom> serializedRooms)
        {
            var roomList = new List<Room>();

            foreach (var serializedRoom in serializedRooms)
            {
                var room = new Room();

                room.InternalName = serializedRoom.InternalName;
                room.Description.Title = serializedRoom.Description.Title;
                room.Description.Body = serializedRoom.Description.Body;

                roomList.Add(room);

                Log.Information($"Room with internal name {room.InternalName} added");
            }

            foreach (var room in roomList)
            {
                var serializedRoom = serializedRooms.Where(r => r.InternalName == room.InternalName).FirstOrDefault();

                if (serializedRoom.Exits != null)
                {
                    foreach (var exit in serializedRoom.Exits)
                    {
                        room.AddExit(roomList.Where(r => r.InternalName == exit.Value).FirstOrDefault(), exit.Key);
                        Log.Information($"Room with internal name {room.InternalName} has a {exit.Key} exit leading to {exit.Value}.");
                    }
                }
            }

            return roomList;
        }
    }
}
