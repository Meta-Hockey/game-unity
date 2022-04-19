using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsButton : MonoBehaviour
    {
        [SerializeField] private Transform buyCardsScrollView;
        [SerializeField] private Transform sellCardsScrollView;
        [SerializeField] private Transform mintNftScrollView;
        [SerializeField] private Transform content;
        
        private BuyCardsController _buyCardsController;
            
        private void Awake()
        {
            _buyCardsController = new BuyCardsController();
        }

        public void LoadNft()
        {
            _buyCardsController.Start();
            
            buyCardsScrollView.gameObject.SetActive(true);
            sellCardsScrollView.gameObject.SetActive(false);
            mintNftScrollView.gameObject.SetActive(false);

            buyCardsScrollView.SetAsLastSibling();
            
            ConstructNftCardsList();
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