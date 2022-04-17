using System.Dynamic;
using System.Threading.Tasks;
using NearClientUnity;

namespace Near.GameContract
{
    public static class Views
    {
        public static async Task<string> GetAvailablePlayers()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic opponents = await gameContract.View("get_available_players", args);

            // TODO: dynamic stuff = JObject.Parse(opponents.result);
            return opponents.result;
        }
        
        public static async Task<string> IsAlreadyInTheList()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.WalletAccount.GetAccountId();

            dynamic isInTheList = await gameContract.View("is_already_in_the_waiting_list", args);
            
            return isInTheList.result;
        }
        
    }
}