using UnityEngine;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardsButton : MonoBehaviour
    {
        [SerializeField] private SellCardsView sellCardsView;

        public void LoadNft()
        {
            sellCardsView.LoadNftCards();
        }
    }
}