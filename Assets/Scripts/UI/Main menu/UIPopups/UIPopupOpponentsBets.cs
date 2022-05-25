using UnityEngine;

namespace UI.Main_menu.UIPopups
{
    public class UIPopupOpponentsBets : UIPopup
    {
        [SerializeField] private UIPopupSetBid uiPopupSetBid;

        public void ShowUIPopupBid()
        {
            uiPopupSetBid.Show();
        }
    }
}