using Near.GameContract;
using Near.GameContract.ContractMethods;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class AvailablePlayers : MonoBehaviour
    {
        [SerializeField] private Text availablePlayersText;
        [SerializeField] private Text isAlreadyInTheWaitingListText;

        public async void UpdateAvailablePlayers()
        {
            // availablePlayersText.text = "Your opponents: " + await Views.GetAvailablePlayers();
            // var nft =  await Near.MarketplaceContract.ContractMethods.Views.LoadCards();
            // availablePlayersText.text = nft[0].owner_id + " " + nft[0].metadata.extra.Type;
        }

        public async void IsAlreadyInTheWaitingList()
        {
            isAlreadyInTheWaitingListText.text = await Views.IsAlreadyInTheList();
        }
    }
}