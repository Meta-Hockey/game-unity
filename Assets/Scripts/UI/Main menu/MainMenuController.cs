using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models;

namespace UI.Main_menu
{
    public class MainMenuController
    {
        public void SetBid(string bid)
        {
            Near.GameContract.ContractMethods.Actions.MakeAvailable(bid);
        }

        public async Task<IEnumerable<Opponent>> GetOpponents()
        {
            return await Near.GameContract.ContractMethods.Views.GetAvailablePlayers();
        }
    }
}