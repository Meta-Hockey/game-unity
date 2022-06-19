using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.FreeAgents.UIPopups
{
    public class UIPopupOnAccept : UIPopup
    {
        [SerializeField] private Text priceText;
        
        public void SetData(string price)
        {
            priceText.text = price;
        }
    }
}