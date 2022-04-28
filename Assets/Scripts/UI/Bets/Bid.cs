using Near.GameContract;
using Near.GameContract.ContractMethods;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class Bid : MonoBehaviour
    {
        [SerializeField] private Text bid;

        public async void SetBid()
        {
            await Actions.MakeAvailable(bid.text);
        }

        public async void CancelTheBid()
        {
            await Actions.MakeUnavailable();
        }
    }
}