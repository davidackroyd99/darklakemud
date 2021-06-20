using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Models
{
    interface IHasDescription
    {
        Description Description { get; set; }
    }

    class Description
    {
        public string Title;
        public string Body;
    }
}
