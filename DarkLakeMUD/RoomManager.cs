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
            character.Room = room;

            var evt = new CharacterEntersRoom(room, character);
            mediator.ReceiveEvent(evt);
        }
    }
}
