using Near.GameContract;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class LoadAvailablePlayers : MonoBehaviour
    {
        [SerializeField] private Text availablePlayersText;
        [SerializeField] private Text isAlreadyInTheWaitingListText;

        private async void Awake()
        {
            availablePlayersText.text = "Your opponents: " + await Views.GetAvailablePlayers();

            isAlreadyInTheWaitingListText.text = await Views.IsAlreadyInTheList();
        }
    }
}