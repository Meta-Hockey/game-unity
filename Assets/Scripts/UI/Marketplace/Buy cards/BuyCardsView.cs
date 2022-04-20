using Runtime;
using UI.Marketplace.Buy_cards.BuyNftCard;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private BuyNftCardView buyNftCardView;


        private BuyCardsController _buyCardsController;

        private bool _isLoaded;

        private void Start()
        {
            _buyCardsController = new BuyCardsController();
            _isLoaded = false;
        }

        public void LoadNftCards()
        {
            viewInteractor.ChangeView(gameObject.transform);
            
            if (_isLoaded)
            {
                return;
            }
            
            // TODO: _buyCardsController.Start();
            
            for (int i = 0; i < 4; i++)
            {
                NftCardUI card = Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerNftCardUI, content);
                
                // TODO: transfer data to card
                card.PrepareNftCard(buyNftCardView);
            }

            _isLoaded = true;
        }
    }
}