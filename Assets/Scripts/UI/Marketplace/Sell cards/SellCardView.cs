using System.Collections.Generic;
using Near.Models;
using NearClientUnity.Utilities;
using UI.Marketplace.Buy_cards;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform sellView;
        [SerializeField] private Transform marketStoragePaidView;
        
        [SerializeField] private Image cardImage;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;
        private string _marketStoragePaid;
        
        [SerializeField] private Toggle isAuction;
        [SerializeField] private InputField price;

        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo)
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

            StartCoroutine(Utils.Utils.LoadImage(cardImage, nftSaleInfo.NFT.metadata.media));
            
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);

            CheckMarketStoragePaid();
        }

        private async void CheckMarketStoragePaid()
        {
            _marketStoragePaid = await viewInteractor.MarketplaceController.GetMarketStoragePaid();

            if (_marketStoragePaid == "0")
            {
                sellView.gameObject.SetActive(false);
                marketStoragePaidView.gameObject.SetActive(true);
            }
            else
            {
                sellView.gameObject.SetActive(true);
                marketStoragePaidView.gameObject.SetActive(false);
            }
        }
        
        public void RegisterStorage()
        {
            viewInteractor.MarketplaceController.RegisterStorage("10");
            CheckMarketStoragePaid();
        }

        public void UpdatePrice()
        {
            UInt128 nearAmount = Near.NearUtils.ParseNearAmount(price.text);
            Dictionary<string, string> newSaleConditions = new Dictionary<string, string> {{"near", nearAmount.ToString()}};
            
            viewInteractor.MarketplaceController.SaleUpdate(newSaleConditions, _nftSaleInfo.NFT.token_id, isAuction.isOn);
        }
    }
}