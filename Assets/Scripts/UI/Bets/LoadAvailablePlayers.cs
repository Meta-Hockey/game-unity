using System.Dynamic;
using NearClientUnity;
using Newtonsoft.Json.Linq;
using Runtime;
using UnityEngine;
using UnityEngine.UI;


namespace UI.Bets
{
    public class LoadAvailablePlayers : MonoBehaviour
    {
        [SerializeField] private Text availablePlayersText;

        struct Bid
        {
            public string Deposit;
            public string OpponentId;
        }
        
        private async void Awake()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic opponents = await gameContract.View("get_available_players", args);
            
            // dynamic stuff = JObject.Parse(opponents.result);
                
            availablePlayersText.text = "Your opponents: " + opponents.result;
            Debug.Log(opponents.result);
        }
    }
}