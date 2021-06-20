using DarkLakeMUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Events
{
    class CharacterEntersRoom
    {
        public Room Room;
        public Character Character;

        public CharacterEntersRoom(Room room, Character character)
        {
            Room = room;
            Character = character;
        }
    }
}
