using DarkLakeMUD.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkLakeMUD
{
    class GameSessionMediator
    {
        // TODO - Clean up old sessions, no need to send updates to them
        private List<GameSession> _sessions;

        public GameSessionMediator()
        {
            _sessions = new List<GameSession>();
        }

        public void AddSession(GameSession session)
        {
            _sessions.Add(session);
        }

        public void ReceiveEvent(CharacterEntersRoom evt)
        {
            var characterSession = _sessions.Where(s => s.Character == evt.Character).FirstOrDefault();
            var sessionsToUpdate = _sessions.Where(s => evt.Room.Characters.Contains(evt.Character) && s.Character != evt.Character);

            // Could be an NPC, hence this could be null
            if (characterSession != null)
                characterSession.SendMessageToClient($"{evt.Room.Description.Title}\n{evt.Room.Description.Body}\n\n");

            foreach (var session in sessionsToUpdate)
                session.SendMessageToClient($"{evt.Character.Name} has entered the room.");
        }
    }
}
