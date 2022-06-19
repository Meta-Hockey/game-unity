using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Sell_cards
{
    public class UIPopupOnSell : UIPopup
    {
        [SerializeField] private Text newPriceText;
        
        public void SetData(string newPrice, bool isAuction)
        {
            if (isAuction)
            {
                title.text = "You have successfully placed the card for auction";
            }
            else
            {
                title.text = "You have successfully placed the card for sale";
            }

            newPriceText.text = "Starting price: " + newPrice;
        }
    }
}