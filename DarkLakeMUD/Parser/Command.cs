using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Parser
{
    public enum Verb
    {
        Go,
        Say
    }

    public class Command
    {
        public Verb Verb;

        public string Noun;
    }
}
