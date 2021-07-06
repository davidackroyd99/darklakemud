using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD.Parser
{
    public interface IParser
    {
        Command Parse(string command);
    }

    public class DefaultParser : IParser
    {
        public Command Parse(string command)
        {
            var components = GetComponents(command);

            return new Command()
            {
                Verb = ParseVerb(components[0]),
                Noun = components[1]
            };
        }

        private string[] GetComponents(string command)
        {
            var split = command.Trim().Split(" ").ToList();
            var noun = string.Join(" ", split.GetRange(1, split.Count - 1).Where(s => s.Length > 0));

            return new string[] { split[0], noun };
        }

        private Verb ParseVerb(string verbString)
        {
            switch (verbString.Trim().ToLower())
            {
                case "go": return Verb.Go;
                case "say": return Verb.Say;
                default: throw new Exception(); // TODO: Parser error handling
            }
        }
    }
}
