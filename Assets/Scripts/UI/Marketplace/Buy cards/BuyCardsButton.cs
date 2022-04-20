using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsButton : MonoBehaviour
    {
        [SerializeField] private Transform buyCardsScrollView;
        [SerializeField] private ViewInteractor viewInteractor;
        
        [SerializeField] BuyCardsView buyCardsView;
            
        private void Start()
        {
            LoadNft();
        }

        public void LoadNft()
        {
            viewInteractor.ChangeView(buyCardsScrollView);
            buyCardsView.LoadNftCards();
        }
    }
}