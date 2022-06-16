using System.Collections;
using Near;
using Near.Models;
using Near.Models.Game.Bid;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu.UIPopups
{
    public class UIPopupSetBid : UIPopup
    {
        [SerializeField] private InputField bidText;

        [SerializeField] private Transform waitForOpponentView;
        [SerializeField] private Transform setBidView;

        [SerializeField] private Text ownBid;

        private bool isWaitForOpponent;
        
        public new async void Show()
        {
            bool isAlreadyInTheList = await mainMenuView.MainMenuController.IsAlreadyInTheList();
            
            if (isAlreadyInTheList)
            {
                GameConfig gameConfig = await mainMenuView.MainMenuController.GetGameConfig();
                ownBid.text = "Your bid: " + NearUtils.FormatNearAmount(UInt128.Parse(gameConfig.Deposit));
                
                waitForOpponentView.gameObject.SetActive(true);
                setBidView.gameObject.SetActive(false);

                isWaitForOpponent = true;
                StartCoroutine(WaitForOpponent());
            }
            else
            {
                isWaitForOpponent = false;
                CheckGame();
                
                waitForOpponentView.gameObject.SetActive(false);
                setBidView.gameObject.SetActive(true);
            }
            
            mainMenuView.ShowPopup(transform);
        }
        
        public void SetBid()
        {
            SetBid(bidText.text);
        }
        
        public void SetBid(string bid)
        {
            mainMenuView.MainMenuController.SetBid(bid);
            // TODO: redirect URL
            Show();
            isWaitForOpponent = true;
            StartCoroutine(WaitForOpponent());
        }

        private IEnumerator WaitForOpponent()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (!isWaitForOpponent)
                {
                    yield break;
                }
                
                CheckGame();
            }
        }

        private async void CheckGame()
        {
            int gameId = await mainMenuView.MainMenuController.GetGameId();

            if (gameId != -1)
            {
                isWaitForOpponent = false;
                Game.LoadGame();    
            }
        }

        public void MakeUnavailable()
        {
            mainMenuView.MainMenuController.MakeUnAvailable();
            isWaitForOpponent = false;
            
            // TODO: redirect URL
            waitForOpponentView.gameObject.SetActive(false);
            setBidView.gameObject.SetActive(true);
        }
    }
}