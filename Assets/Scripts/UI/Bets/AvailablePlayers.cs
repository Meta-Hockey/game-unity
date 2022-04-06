using System.Dynamic;
using NearClientUnity;
using Runtime;
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
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.from_index = 0;
            args.limit = 50;

            dynamic opponents = await gameContract.View("get_available_players", args);
            
            // dynamic stuff = JObject.Parse(opponents.result);
                
            availablePlayersText.text = "Your opponents: " + opponents.result;
            Debug.Log(opponents.result);
        }

        public async void IsAlreadyInTheWaitingList()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
            
            dynamic args = new ExpandoObject();
            args.account_id = NearPersistentManager.Instance.WalletAccount.GetAccountId();

            dynamic isInTheList = await gameContract.View("is_already_in_the_waiting_list", args);

            isAlreadyInTheWaitingListText.text = isInTheList.result;
        }
    }
}