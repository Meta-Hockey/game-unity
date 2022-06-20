using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models.Game;

namespace UI.GameUI
{
    public class GameController
    {
        public List<Event> Events;

        public int NumberOfGeneratedEvents;

        private bool isGenerated = false;

        public GameController()
        {
            Events = new List<Event>();
            NumberOfGeneratedEvents = 0;
        }
        
        public async Task<int> GetGameId()
        {
            return await Near.GameContract.ContractMethods.Views.GetGameId();
        }

        public async void GenerateEvents(int gameId)
        {
            if (isGenerated)
            {
                return;
            }

            isGenerated = true;

            try
            {
                List<Event> events = await Near.GameContract.ContractMethods
                    .Actions.GenerateEvent(NumberOfGeneratedEvents, gameId);

                if (events != null)
                {
                    NumberOfGeneratedEvents += events.Count;
                    Events.AddRange(events);

                    isGenerated = false;
                }
            }
            catch
            {
                isGenerated = false;
                GenerateEvents(gameId);
            }
        }
    }
}