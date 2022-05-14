using System.Threading.Tasks;
using Near.Models.Team.Team;

namespace UI.ManageTeam
{
    public class ManageTeamController
    {
        public async Task<Team> LoadUserTeam()
        {
            return await Near.GameContract.ContractMethods.Views.LoadUserTeam();
        }
    }
}