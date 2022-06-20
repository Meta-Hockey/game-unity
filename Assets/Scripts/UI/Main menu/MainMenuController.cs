using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models;
using Near.Models.Game.Bid;

namespace UI.Main_menu
{
    public class MainMenuController
    {
        public void StartGame(string opponentId, string deposit)
        {
            Near.GameContract.ContractMethods.Actions.StartGame(opponentId, deposit);
        }

        public async Task<int> GetGameId()
        {
            return await Near.GameContract.ContractMethods.Views.GetGameId();
        }
        
        public void SetBid(string bid)
        {
            Near.GameContract.ContractMethods.Actions.MakeAvailable(bid);
        }

        public void MakeUnAvailable()
        {
            Near.GameContract.ContractMethods.Actions.MakeUnavailable();
        }

        public async Task<IEnumerable<Opponent>> GetOpponents()
        {
            return await Near.GameContract.ContractMethods.Views.GetAvailablePlayers();
        }

        public async Task<bool> IsAlreadyInTheList()
        {
            return await Near.GameContract.ContractMethods.Views.IsAlreadyInTheList();
        }

        public async Task<GameConfig> GetGameConfig()
        {
            return await Near.GameContract.ContractMethods.Views.GetGameConfig();
        }
    }
}