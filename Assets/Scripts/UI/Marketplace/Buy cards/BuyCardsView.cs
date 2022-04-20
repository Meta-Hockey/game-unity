using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsView : MonoBehaviour, IViewNftCards
    {
        [SerializeField] private Transform buyCardView;
        [SerializeField] private Transform content;

        [SerializeField] private ViewInteractor viewInteractor;

        private BuyCardsController _buyCardsController;

        private bool _isLoaded;

        private void Start()
        {
            _buyCardsController = new BuyCardsController();
            _isLoaded = false;
        }

        public void LoadNftCards()
        {
            if (_isLoaded)
            {
                return;
            }
            
            // _buyCardsController.Start();
            
            for (int i = 0; i < 4; i++)
            {
                NftCardInfoUI card = Instantiate(Game.AssetRoot.marketplaceAsset.nftCardInfoUI, content);
                card.SetNftCard(OnClickCard);
            }

            _isLoaded = true;
        }

        private void OnClickCard()
        {
            viewInteractor.ChangeView(buyCardView);
        }
    }
}