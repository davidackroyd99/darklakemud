using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Models
{
    // A concious entity within the game. Can be PC or NPC.
    class Character
    {
        public string Name;
        public int Level = 1;
        public Room Room;

        public Character()
        {
            Room = new Room();
            Room.Description.Title = "The Top Of The Jetty";
            Room.Description.Body = "You are standing at the top of the jetty. ";
        }
    }
}
