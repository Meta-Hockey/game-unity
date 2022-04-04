using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class CompleteSignInButton : MonoBehaviour
    {
        [SerializeField] private Text inputUri;

        [SerializeField] private Text accountId;
        
        public async void Complete()
        {
            Debug.Log(inputUri.text);
            await NearPersistentManager.Instance.WalletAccount.CompleteSignIn(inputUri.text);
            if(NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                accountId.text = "Your account id: " + NearPersistentManager.Instance.WalletAccount.GetAccountId(); 
            }        
        }
    }
}