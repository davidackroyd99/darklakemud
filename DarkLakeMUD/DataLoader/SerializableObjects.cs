using DarkLakeMUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DarkLakeMUD.DataLoader
{
    public class SerializableRoom
    {
        public string InternalName { get; set; }

        public SerializableDescription Description { get; set; }

        public Dictionary<Direction, string> Exits { get; set; }
    }

    public class SerializableDescription
    {
        public string Title { get; set; }

        public string Body { get; set; }
    }
}
