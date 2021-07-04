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

    public class Description
    {
        public string Title;
        public string Body;
    }
}
