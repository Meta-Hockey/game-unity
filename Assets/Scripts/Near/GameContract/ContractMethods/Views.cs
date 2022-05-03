using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Near.Models.Team;
using Near.Models.Team.Metadata;
using NearClientUnity;
using Newtonsoft.Json;

namespace Near.GameContract.ContractMethods
{
    public static class Views
    {
        public class Opponent
        {
            public string Name { get; set; }
            public GameConfig GameConfig { get; set; }
        }

        public class GameConfig
        {
            public string OpponentId { get; set; }
            public string Deposit { get; set; }
        }
        
        public static async Task<IEnumerable<Opponent>> GetAvailablePlayers()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();

            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic opponents = await gameContract.View("get_available_players", args);

            List<List<object>> objects = JsonConvert
                .DeserializeObject<List<List<object>>>(opponents.result.ToString());
            
            var result = objects.Select(o => new Opponent
            {
                Name = (string) o[0],
                GameConfig = JsonConvert.DeserializeObject<GameConfig>(o[1].ToString())
            }); 
            
            return result;
        }
        
        public static async Task<string> IsAlreadyInTheList()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
                
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.WalletAccount.GetAccountId();

            dynamic isInTheList = await gameContract.View("is_already_in_the_waiting_list", args);
            
            return isInTheList.result;
        }

        public static async Task<(TeamIds, TeamMetadata)> LoadUserTeam()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetGameContract();
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.GetAccountId();
            
            dynamic ownerTeamResult = await gameContract.Change("get_owner_team", args, NearUtils.Gas);
            
            (Dictionary<string, dynamic>, Dictionary<string, dynamic>) dictionaries =
                JsonConvert.DeserializeObject<(Dictionary<string, dynamic>, Dictionary<string, dynamic>)>(
                    ownerTeamResult.ToString());
            
            TeamIds teamIds = new TeamIds();
            if (ownerTeamResult != null)
            {
                dynamic ownerTeamIdsResult = await nftContract.View("get_owner_nft_team", args);
                teamIds = JsonConvert.DeserializeObject<TeamIds>(ownerTeamIdsResult.result);
            }
            
            return (null, null);
        }
    }
}