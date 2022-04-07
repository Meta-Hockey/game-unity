using Near.GameContract;
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
            availablePlayersText.text = "Your opponents: " + await Views.GetAvailablePlayers();
        }

        public async void IsAlreadyInTheWaitingList()
        {
            isAlreadyInTheWaitingListText.text = await Views.IsAlreadyInTheList();
        }
    }
}