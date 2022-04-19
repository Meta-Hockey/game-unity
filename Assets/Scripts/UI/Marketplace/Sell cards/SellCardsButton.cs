using Runtime;
using UnityEngine;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardsButton : MonoBehaviour
    {
        [SerializeField] private Transform buyCardsScrollView;
        [SerializeField] private Transform sellCardsScrollView;
        [SerializeField] private Transform mintNftScrollView;
        [SerializeField] private Transform content;

        public void LoadNft()
        {
            sellCardsScrollView.gameObject.SetActive(true);
            buyCardsScrollView.gameObject.SetActive(false);
            mintNftScrollView.gameObject.SetActive(false);

            sellCardsScrollView.SetAsLastSibling();
            
            ConstructNftCardsList();
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