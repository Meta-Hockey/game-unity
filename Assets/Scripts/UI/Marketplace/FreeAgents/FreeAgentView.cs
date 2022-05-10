using System.Collections.Generic;
using Near;
using Near.Models;
using NearClientUnity.Utilities;
using Runtime;
using UI.Marketplace.Buy_cards;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.FreeAgents
{
    public class FreeAgentView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform bidsView;
        [SerializeField] private Transform buyImmediatelyView;
        
        [SerializeField] private Transform bidContent;
            
        [SerializeField] private Image cardImage;
        [SerializeField] private Text price;
        
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        [SerializeField] private Transform setNewPriceView;

        [SerializeField] private Text currentSaleConditions;
        [SerializeField] private InputField newSaleConditions;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;

        private string _price;
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

            StartCoroutine(Utils.Utils.LoadImage(cardImage, nftSaleInfo.NFT.metadata.media));
            
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);
            _nftSaleInfo = nftSaleInfo;
            
            if (nftSaleInfo.Sale is {is_auction: false})
            {
                buyImmediatelyView.gameObject.SetActive(true);
                bidsView.gameObject.SetActive(false);

                _price = NearUtils.FormatNearAmount(UInt128.Parse(nftSaleInfo.Sale.sale_conditions["near"])).ToString();
                price.text = "Price: " + _price;
                currentSaleConditions.text = _price;
            }
            else
            {
                buyImmediatelyView.gameObject.SetActive(false);
                bidsView.gameObject.SetActive(true);

                if (!nftSaleInfo.Sale.sale_conditions.ContainsKey("near"))
                {
                    return;
                }
                
                BidText saleConditionText = Instantiate(Game.AssetRoot.marketplaceAsset.bid, bidContent);

                saleConditionText.bid.text = "Sale conditions" + ":  " + NearUtils.FormatNearAmount(UInt128.Parse(_nftSaleInfo.Sale.sale_conditions["near"]));

                currentSaleConditions.text = saleConditionText.bid.text;
                
                if (!nftSaleInfo.Sale.bids.ContainsKey("near"))
                {
                    return;
                }
                
                foreach (Bid saleBid in nftSaleInfo.Sale.bids["near"])
                {
                    BidText bidText = Instantiate(Game.AssetRoot.marketplaceAsset.bid, bidContent);

                    string ownerId = saleBid.owner_id != NearPersistentManager.Instance.GetAccountId() ? saleBid.owner_id : "Your bid";
                    bidText.bid.text = ownerId + ":  " + NearUtils.FormatNearAmount(UInt128.Parse(saleBid.price));
                }
            }
        }

        public void AcceptOffer()
        {
            viewInteractor.MarketplaceController.AcceptOffer(_nftSaleInfo.NFT.token_id);
        }

        public void ShowSetNewPriceView()
        {
            setNewPriceView.gameObject.SetActive(true);
        }

        public void CloseSetNewPriceView()
        {
            setNewPriceView.gameObject.SetActive(false);
        }

        public void UpdatePrice()
        {
            UInt128 nearAmount = NearUtils.ParseNearAmount(newSaleConditions.text);

            Dictionary<string, string> newSale = new Dictionary<string, string> {{"near", nearAmount.ToString()}};
            viewInteractor.MarketplaceController.SaleUpdate(newSale, _nftSaleInfo.NFT.token_id, _nftSaleInfo.Sale.is_auction);
            
            CloseSetNewPriceView();
        }
    }
}