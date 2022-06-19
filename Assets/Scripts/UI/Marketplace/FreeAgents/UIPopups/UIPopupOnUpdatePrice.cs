using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.FreeAgents.UIPopups
{
    public class UIPopupOnUpdatePrice : UIPopup
    {
        [SerializeField] private Text newPriceText;
        
        public void SetData(string price)
        {
            newPriceText.text = "New price: " + price;
        }
    }
}