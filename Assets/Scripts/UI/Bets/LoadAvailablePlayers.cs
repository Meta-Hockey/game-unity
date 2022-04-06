using System.Dynamic;
using NearClientUnity;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class LoadAvailablePlayers : MonoBehaviour
    {
        [SerializeField] private Text availablePlayersText;

        private async void Awake()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            var opponents = await gameContract.View("get_available_players", args);
                
            availablePlayersText.text = "Your opponents: " + opponents.result;
        }
    }
}