namespace UI.ManageTeam
{
    public class ManageTeamController
    {
        public async void LoadUserTeam()
        {
            await Near.GameContract.ContractMethods.Views.LoadUserTeam();
        }
    }
}