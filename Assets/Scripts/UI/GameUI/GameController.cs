using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models.Game;

namespace UI.GameUI
{
    public class GameController
    {
        public List<Event> Events;

        public int NumberOfGeneratedEvents;

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
            List<Event> events =  await Near.GameContract.ContractMethods
                .Actions.GenerateEvent(NumberOfGeneratedEvents, gameId);

            NumberOfGeneratedEvents += events.Count;
            Events.AddRange(events);
        }
    }
}