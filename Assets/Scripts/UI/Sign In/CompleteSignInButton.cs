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
                SceneManager.UnloadSceneAsync(Game.AssetRoot.signInUIScene.name);
                SceneManager.LoadScene(Game.AssetRoot.mainMenuUIScene.name, LoadSceneMode.Additive);
            }   
        }
    }
}