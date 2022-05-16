using System;
using Near;
using NearClientUnity;
using NearClientUnity.Utilities;
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
        [SerializeField] private Text balance;

        [SerializeField] private Transform mintNFTButton;

        public async void LoadAccountId()
        {
            string accountID = NearPersistentManager.Instance.GetAccountId();
            
            accountId.text = "Welcome, " + accountID + " !";

            AccountState accountState = await NearPersistentManager.Instance.GetAccountState();
            balance.text = "Your balance: " + NearUtils.FormatNearAmount(UInt128.Parse(accountState.Amount)) + " NEAR";
            
            if (accountID == NearPersistentManager.Instance.MarketplaceContactId)
            {
                mintNFTButton.gameObject.SetActive(true);
            }
        }
        
        public void LoadBetsScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bets");
            operation.completed += LoadBetsUI;
        }

        private void LoadBetsUI(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception("Can't load scene");
            }

            SceneManager.LoadScene("BetsUI", LoadSceneMode.Additive);
        }

        public void TradeCards()
        {
            Game.LoadMarketplace();
        }

        public void LoadMintNFT()
        {
            SceneManager.LoadScene("Mint NFT");
        }

        public void LoadManageTeam()
        {
            SceneManager.LoadScene("ManageTeam"); 
        }
        
        public void SignOut()
        {
            NearPersistentManager.Instance.SignOut();
            
            gameObject.SetActive(false);
            signInView.gameObject.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}