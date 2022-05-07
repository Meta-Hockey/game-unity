using System;
using Near;
using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Main_menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private SignInView signInView;
        
        [SerializeField] private Text accountId;

        public void LoadAccountId()
        {
            accountId.text = "Welcome: " + NearPersistentManager.Instance.GetAccountId() + " !";
        }
        
        public void LoadBetsScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(Game.AssetRoot.betsScene.name);
            operation.completed += LoadBetsUI;
        }

        private void LoadBetsUI(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception("Can't load scene");
            }
            
            SceneManager.LoadScene(Game.AssetRoot.betsUIScene.name, LoadSceneMode.Additive);
        }

        public void TradeCards()
        {
            Game.LoadMarketplace();
        }

        public void LoadMintNFT()
        {
            SceneManager.LoadScene(Game.AssetRoot.mintNFT.name);
        }

        public void LoadManageTeam()
        {
            SceneManager.LoadScene(Game.AssetRoot.manageTeamScene.name); 
        }
        
        public void SignOut()
        {
            NearPersistentManager.Instance.SignOut();
            
            gameObject.SetActive(false);
            signInView.gameObject.SetActive(true);
        }
    }
}