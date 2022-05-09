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
        [SerializeField] private Image cardImage;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;

        [SerializeField] private Text marketStoragePaid;
        [SerializeField] private InputField amountSpots;

        [SerializeField] private Toggle isAuction;
        [SerializeField] private InputField price;

        public async void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo, Image image)
        {
            viewInteractor.ChangeView(gameObject.transform);

            if (_cardTile != null)
            {
                Destroy(_cardTile.gameObject);
            }

            if (_cardDescription != null)
            {
                Destroy(_cardDescription.gameObject);
            }

            _nftSaleInfo = nftSaleInfo;

            cardImage = image;
            
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);
            
           // marketStoragePaid.text = "Market storage paid: " +  await viewInteractor.MarketplaceController.GetMarketStoragePaid();
        }

        public void RegisterStorage()
        {
            viewInteractor.MarketplaceController.RegisterStorage(amountSpots.text);
        }

        public void UpdatePrice()
        {
            UInt128 nearAmount = Near.NearUtils.ParseNearAmount(price.text);
            Dictionary<string, string> newSaleConditions = new Dictionary<string, string> {{"near", nearAmount.ToString()}};
            
            viewInteractor.MarketplaceController.SaleUpdate(newSaleConditions, _nftSaleInfo.NFT.token_id, isAuction.isOn);
        }
    }
}