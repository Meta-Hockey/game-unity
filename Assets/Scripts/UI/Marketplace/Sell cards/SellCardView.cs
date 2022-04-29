using System.Collections.Generic;
using Near.Models;
using NearClientUnity.Utilities;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform cardTileContent;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;

        [SerializeField] private Text marketStoragePaid;
        [SerializeField] private InputField amountSpots;

        [SerializeField] private InputField isAuction;
        [SerializeField] private InputField price;
        [SerializeField] private InputField ftId;
        

        public async void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo)
        {
            if (_cardTile != null)
            {
                Destroy(_cardTile.gameObject);
            }

            if (_cardDescription != null)
            {
                Destroy(_cardDescription.gameObject);
            }

            _nftSaleInfo = nftSaleInfo;
                
            _cardTile = cardRenderer.RenderCardTile(cardTileContent);
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);
            
            marketStoragePaid.text = "Market storage paid: " +  await viewInteractor.MarketplaceController.GetMarketStoragePaid();
            
            viewInteractor.ChangeView(gameObject.transform);
            
            StartCoroutine(Utils.Utils.LoadImage(_cardDescription.Image, _nftSaleInfo.NFT.metadata.media));
        }

        public void RegisterStorage()
        {
            viewInteractor.MarketplaceController.RegisterStorage(amountSpots.text);
        }

        public void UpdatePrice()
        {
            UInt128 nearAmount = Near.NearUtils.ParseNearAmount(price.text);
            Dictionary<string, UInt128> newSaleConditions = new Dictionary<string, UInt128> {{ftId.text, nearAmount}};
            
            viewInteractor.MarketplaceController.SaleUpdate(newSaleConditions, _nftSaleInfo.NFT.token_id, bool.Parse(isAuction.text));
        }
    }
}