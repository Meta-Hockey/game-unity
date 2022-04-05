using System.Dynamic;
using NearClientUnity;
using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Sign_In
{
    public class CompleteSignInButton : MonoBehaviour
    {
        [SerializeField] private Text inputUri;

        public async void CompleteSignIn()
        {
            await NearPersistentManager.Instance.WalletAccount.CompleteSignIn(inputUri.text);
            if(NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                /*
                ContractNear gameContract = await NearPersistentManager.Instance.GetContract();
                
                dynamic args = new ExpandoObject();
                args.from_index = 0;
                args.limit = 50;

                var opponents = await gameContract.View("get_available_players", args);
                
                opponentsText.text = "Your opponents: " + opponents.result;
                */
                
                SceneManager.UnloadSceneAsync(Game.AssetRoot.signInUIScene.name);
                SceneManager.LoadScene(Game.AssetRoot.mainMenuUIScene.name, LoadSceneMode.Additive);
            }   
        }
    }
}