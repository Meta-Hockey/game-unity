using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Near.GameContract.ContractMethods;
using Near.Models;
using Near.Models.Team.Team;

namespace UI.ManageTeam
{
    public class ManageTeamController
    {
        public async Task<Team> LoadUserTeam()
        {
            return await Views.LoadUserTeam();
        }

        public async Task<List<NFTMetadata>> LoadUserNFTs()
        {
            List<NFTSaleInfo> userNFTs = await Near.MarketplaceContract.ContractMethods.Views.LoadUserNFTs();

            return userNFTs.Select(x => new NFTMetadata()
            {
                Metadata = x.NFT.metadata,
                Id = x.NFT.token_id
            }).ToList();
        }

        public void ChangeLineups(Team team)
        {
            Actions.ChangeLineups(team);
        }
    }
}