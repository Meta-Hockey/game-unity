using System.Dynamic;
using NearClientUnity;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bets
{
    public class SetBidButton : MonoBehaviour
    {
        [SerializeField] private Text bid;

        public async void SetBid()
        {
            /*
            ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
            dynamic args = new ExpandoObject();
            args.config = new Object();

            gameContract.Change("make_available", args, NearPersistentManager.Instance.GasMakeAvailable);
            */

            var near = NearPersistentManager.Instance.ParseNearAmount(bid.text);
            Debug.Log(near);
            Debug.Log(NearPersistentManager.Instance.FormatNearAmount(near));
            bid.text = "near: " + near;
        }
    }
}