using Near;
using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Main_menu
{
    public class SignOutButton : MonoBehaviour
    {
        public void SignOut()
        {
            NearPersistentManager.Instance.SignOut();
            
            SceneManager.UnloadSceneAsync(Game.AssetRoot.mainMenuUIScene.name);
            SceneManager.LoadScene(Game.AssetRoot.signInUIScene.name, LoadSceneMode.Additive);
        }
    }
}