using Runtime;
using UnityEngine;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardsButton : MonoBehaviour
    {
        [SerializeField] private Transform sellCardsScrollView;
        [SerializeField] private Transform content;
        
        [SerializeField] private ViewInteractor viewInteractor;

        private bool _isNftLoaded;

        public void LoadNft()
        {
            viewInteractor.ChangeView(sellCardsScrollView);

            if (!_isNftLoaded)
            {
                ConstructNftCardsList();
                _isNftLoaded = true;
            }
        }
        
        private void ConstructNftCardsList()
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(Game.AssetRoot.marketplaceAsset.nftCardInfoUI, content);
            }
        }
    }
}