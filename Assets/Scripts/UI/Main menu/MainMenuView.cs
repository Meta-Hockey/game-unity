using System;
using System.Collections.Generic;
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

        [SerializeField] private List<Transform> popups;

        [SerializeField] private Transform mintNFTButton;

        public MainMenuController MainMenuController;

        private void Start()
        {
            MainMenuController = new MainMenuController();
        }

        public async void LoadAccountId()
        {
            string accountID = NearPersistentManager.Instance.GetAccountId();
            
            accountId.text = "Welcome, " + accountID + " !";

            AccountState accountState = await NearPersistentManager.Instance.GetAccountState();
            balance.text = "Your balance: " + NearUtils.FormatNearAmount(UInt128.Parse(accountState.Amount)) + " NEAR";

            mintNFTButton.gameObject.SetActive(accountID == NearPersistentManager.Instance.MarketplaceContactId);
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

        public void ShowPopup(Transform popupTransform)
        {
            foreach (Transform popup in popups)
            {
                popup.gameObject.SetActive(false);
            }
            
            popupTransform.gameObject.SetActive(true);
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