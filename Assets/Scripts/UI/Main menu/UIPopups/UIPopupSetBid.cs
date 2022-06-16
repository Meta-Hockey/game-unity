using System.Collections;
using Near;
using Near.Models;
using NearClientUnity.Utilities;
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

        public new async void Show()
        {
            bool isAlreadyInTheList = await mainMenuView.MainMenuController.IsAlreadyInTheList();
            
            if (isAlreadyInTheList)
            {
                GameConfig gameConfig = await mainMenuView.MainMenuController.GetGameConfig();
                ownBid.text = "Your bid: " + NearUtils.FormatNearAmount(UInt128.Parse(gameConfig.Deposit));
                
                waitForOpponentView.gameObject.SetActive(true);
                setBidView.gameObject.SetActive(false);
            }
            else
            {
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
            
            
        }

        private IEnumerable CheckGame()
        {
            yield return new  WaitForSeconds(60);
        }

        public void MakeUnavailable()
        {
            mainMenuView.MainMenuController.MakeUnAvailable();
            // TODO: redirect URL
            waitForOpponentView.gameObject.SetActive(false);
            setBidView.gameObject.SetActive(true);
        }
    }
}