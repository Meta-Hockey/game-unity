using Near;
using Near.Models;
using NearClientUnity.Utilities;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Text price;
        
        [SerializeField] private Transform cardTileContent;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;

        private string _price;
        

        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo)
        {
            if (_cardTile != null)
            {
                Destroy(_cardTile.gameObject);
            }
            
            if (_cardDescription != null)
            {
                Destroy(_cardDescription.gameObject);
            }
            
            _cardTile = cardRenderer.RenderCardTile(cardTileContent);
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);
            _nftSaleInfo = nftSaleInfo;
            
            if (nftSaleInfo.Sale is {is_auction: false})
            {
                _price = NearUtils.FormatNearAmount(UInt128.Parse(nftSaleInfo.Sale.sale_conditions["near"])).ToString();
                price.text = "Buy for " + _price + " NEAR";
            }
            
            viewInteractor.ChangeView(gameObject.transform);
        }

        public void BuyCard()
        {
            viewInteractor.MarketplaceController.Offer(_nftSaleInfo.NFT.token_id, "near", _price);
        }
    }
}