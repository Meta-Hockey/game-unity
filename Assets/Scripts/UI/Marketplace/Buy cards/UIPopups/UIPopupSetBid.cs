using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Buy_cards.UIPopups
{
    public class UIPopupSetBid : UIPopup
    {
        [SerializeField] private Text bidText;
        
        public void SetData(string bid)
        {
            bidText.text = "Your bid: " + bid;
        }
    }
}