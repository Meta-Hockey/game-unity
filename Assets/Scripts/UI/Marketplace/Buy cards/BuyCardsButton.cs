using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsButton : MonoBehaviour
    {
        [SerializeField] BuyCardsView buyCardsView;
            
        private void Start()
        {
            LoadNft();
        }

        public void LoadNft()
        {
            buyCardsView.LoadNftCards();
        }
    }
}