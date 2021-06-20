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
        
        public Room()
        {
            Description = new Description();
        }
    }
}
