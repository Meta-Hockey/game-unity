using UnityEngine;
using UnityEngine.UI;

namespace UI.Main_menu.UIPopups
{
    public class UIPopupSetBid : UIPopup
    {
        [SerializeField] private InputField bidText;
        [SerializeField] private MainMenuView view;
        
        public void SetBid()
        {
            SetBid(bidText.text);
        }
        
        public void SetBid(string bid)
        {
            view.MainMenuController.SetBid(bid);
        }
    }
}