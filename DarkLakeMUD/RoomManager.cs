using DarkLakeMUD.Events;
using DarkLakeMUD.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    class RoomManager
    {
        List<Room> _rooms;

        public RoomManager()
        {
            _rooms = new List<Room>();
        }

        public void AddRooms(List<Room> rooms)
        {
            foreach (var room in rooms)
                AddRoom(room);
        }

        public void AddRoom(Room room) => _rooms.Add(room);

        public List<Room> GetRooms() => _rooms;

        public void AddCharacterToRoom(Room room, Character character, GameSessionMediator mediator)
        {
            var evt = new CharacterEntersRoom(room, character);

            Log.Debug($"Adding character {character.Name} to room {room.InternalName}.");

            lock (room)
                room.AddCharacter(character);

            mediator.ReceiveEvent(evt);
        }

        public void RemoveCharacterToRoom(Room room, Character character, GameSessionMediator mediator)
        {
            var evt = new CharacterEntersRoom(room, character);

            Log.Debug($"Adding character {character.Name} to room {room.InternalName}.");

            lock (room)
                room.AddCharacter(character);

            mediator.ReceiveEvent(evt);
        }

        public void MoveCharacter(Character character, Direction direction, GameSessionMediator mediator)
        {
            var room = _rooms.Where(r => r.Characters.Contains(character)).FirstOrDefault();
            var destination = room.GetExit(direction);

            Log.Debug($"Character {character.Name} moving from {room.InternalName} to {destination.InternalName}.");

            if (destination != null)
            {
                lock (room) lock (destination)
                {
                    room.Characters.Remove(character);
                    AddCharacterToRoom(destination, character, mediator);
                }
            }
        }

        public void EvictCharacter(Character character)
        {
            var characterRooms = _rooms.Where(r => r.Characters.Contains(character));

            // TODO lock
            foreach (var r in characterRooms)
                r.Characters.Remove(character);
        }
    }
}
