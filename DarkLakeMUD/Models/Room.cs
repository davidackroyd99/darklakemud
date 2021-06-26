using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Models
{
    class Room : IHasDescription
    {
        public Description Description { get; set; }
        public List<Character> Characters { get; set; }
        
        public Room()
        {
            Description = new Description();
            Characters = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }
    }
}
