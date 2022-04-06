using System.Dynamic;
using NearClientUnity;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class Bid : MonoBehaviour
    {
        [SerializeField] private Text bid;

        public async void SetBid()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.config = new Object();

            await gameContract.Change("make_available", args,
                NearPersistentManager.
                Instance.
                GasMakeAvailable,
                NearPersistentManager.Instance.ParseNearAmount(bid.text));
        }

        public async void CancelTheBid()
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();

            await gameContract.Change("make_unavailable", args);
        }
    }
}