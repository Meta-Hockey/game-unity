using System.Collections.Generic;
using System.Threading.Tasks;
using Near.GameContract;
using Near.GameContract.ContractMethods;
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
            IEnumerable<Views.Opponent> opponents = await Views.GetAvailablePlayers();

            Debug.Log(opponents);
            
            foreach (Views.Opponent opponent in opponents)
            {
                
            }
            
            // availablePlayersText.text = "Your opponents: " + await Views.GetAvailablePlayers();

            isAlreadyInTheWaitingListText.text = await Views.IsAlreadyInTheList();
        }
    }
}