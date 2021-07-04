using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Models
{
    public enum Direction
    {
        [EnumMember(Value = "North")]
        NORTH,
        [EnumMember(Value = "South")]
        SOUTH,
        [EnumMember(Value = "East")]
        EAST,
        [EnumMember(Value = "West")]
        WEST,
        [EnumMember(Value = "Up")]
        UP,
        [EnumMember(Value = "Down")]
        DOWN
    };

    public class Room : IHasDescription
    {
        public string InternalName { get; set; }
        public Description Description { get; set; }
        public List<Character> Characters { get; set; }
        public Dictionary<Direction, Room> Exits { get; set; }

        public Room()
        {
            Description = new Description();
            Characters = new List<Character>();
            Exits = new Dictionary<Direction, Room>();
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        public Room GetExit(Direction direction)
        {
            if (Exits.ContainsKey(direction))
                return Exits[direction];

            return null;
        }

        public void AddExit(Room room, Direction direction)
        {
            Exits.Add(direction, room);
        }
    }
}
