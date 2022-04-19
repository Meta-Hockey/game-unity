using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsButton : MonoBehaviour
    {
        [SerializeField] private Transform buyCardsScrollView;
        [SerializeField] private Transform content;
        
        [SerializeField] private ViewInteractor viewInteractor;
        
        private BuyCardsController _buyCardsController;
        
        private bool _isNftLoaded;
            
        private void Start()
        {
            _buyCardsController = new BuyCardsController();
            LoadNft();
        }

        public void LoadNft()
        {
            viewInteractor.ChangeView(buyCardsScrollView);
            
            if (!_isNftLoaded)
            {
                _buyCardsController.Start();
                ConstructNftCardsList();
                
                _isNftLoaded = true;
            }
        }
        
        private void ConstructNftCardsList()
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(Game.AssetRoot.marketplaceAsset.nftCardInfoUI, content);
                
            }
        }
    }
}