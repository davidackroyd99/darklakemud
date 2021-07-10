using DarkLakeMUD.Models;

namespace DarkLakeMUD.Events
{
    public class CharacterEntersRoom
    {
        public Room Room;
        public Character Character;
        public Direction Direction;

        public CharacterEntersRoom(Room room, Character character, Direction direction)
        {
            Room = room;
            Character = character;
            Direction = direction;
        }
    }
}
