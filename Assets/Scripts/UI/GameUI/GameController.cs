using System.Threading.Tasks;

namespace UI.GameUI
{
    public class GameController
    {
        public async Task<int> GetGameId()
        {
            return await Near.GameContract.ContractMethods.Views.GetGameId();
        }

        public async Task<dynamic> GenerateEvents(int numberOfRenderedEvents, int gameId)
        {
            return await Near.GameContract.ContractMethods.Actions.GenerateEvent(numberOfRenderedEvents, gameId);
        }
    }
}