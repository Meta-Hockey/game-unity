namespace UI.Main_menu
{
    public class MainMenuController
    {
        public void SetBid(string bid)
        {
            Near.GameContract.ContractMethods.Actions.MakeAvailable(bid);
        }
    }
}