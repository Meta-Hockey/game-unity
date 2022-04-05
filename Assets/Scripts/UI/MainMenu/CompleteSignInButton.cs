using System.Dynamic;
using NearClientUnity;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class CompleteSignInButton : MonoBehaviour
    {
        [SerializeField] private Text inputUri;

        [SerializeField] private Text accountId;
        
        [SerializeField] private Text opponentsText;
        
        
        public async void Complete()
        {
            Debug.Log(inputUri.text);
            await NearPersistentManager.Instance.WalletAccount.CompleteSignIn(inputUri.text);
            if(NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
                dynamic args = new ExpandoObject();
                args.from_index = 0;
                args.limit = 50;

                var opponents = await gameContract.View("get_available_players", args);

                dynamic r = opponents.result;
                
                opponentsText.text = "Your opponents: " + r + "  ...";
                

                accountId.text = "Your account id: " + NearPersistentManager.Instance.WalletAccount.GetAccountId();
            }   
        }
    }
}