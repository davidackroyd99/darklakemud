using DarkLakeMUD.Events;
using DarkLakeMUD.Models;
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

        public void AddRoom(Room room)
        {
            _rooms.Add(room);
        }

        public List<Room> GetRooms()
        {
            return _rooms;
        }

        public void AddCharacterToRoom(Room room, Character character, GameSessionMediator mediator)
        {
            var evt = new CharacterEntersRoom(room, character);

            lock (room)
            {
                room.AddCharacter(character);
            }

            mediator.ReceiveEvent(evt);
        }

        public void MoveCharacter(Character character, Direction direction, GameSessionMediator mediator)
        {
            var room = _rooms.Where(r => r.Characters.Contains(character)).FirstOrDefault();
            var destination = room.GetExit(direction);

            if (destination != null)
            {
                lock (room) lock (destination)
                {
                    room.Characters.Remove(character);
                    room.AddCharacter(character);
                }
            }

            mediator.ReceiveEvent(new CharacterEntersRoom(destination, character));
        }
    }
}
