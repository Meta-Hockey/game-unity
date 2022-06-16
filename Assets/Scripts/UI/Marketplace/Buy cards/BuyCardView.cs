using System.Collections.Generic;
using Near;
using Near.Models;
using Near.Models.Game;
using Near.Models.Game.Bid;
using NearClientUnity.Utilities;
using Runtime;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform setBidView;
        [SerializeField] private Transform buyImmediatelyView;
        
        [SerializeField] private Transform bidContent;
            
        [SerializeField] private Image cardImage;
        [SerializeField] private Text price;
        
        [SerializeField] private Button buyButton;
        [SerializeField] private Text buyButtonText;
        
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;
        
        [SerializeField] private InputField bid;

        private List<BidText> _bidTexts;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;
        private NFTSaleInfo _nftSaleInfo;

        private string _price;
        

        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo)
        {
            viewInteractor.ChangeView(gameObject.transform);

            if (_bidTexts != null)
            {
                foreach (BidText bidText in _bidTexts)
                {
                    Destroy(bidText.gameObject);
                }
            }
            
            _bidTexts = new List<BidText>();

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

            if (nftSaleInfo.NFT.owner_id == NearPersistentManager.Instance.GetAccountId())
            {
                buyButton.gameObject.SetActive(false);
                return;
            }
            
            if (nftSaleInfo.Sale is {is_auction: false})
            {
                buyImmediatelyView.gameObject.SetActive(true);
                setBidView.gameObject.SetActive(false);
                buyButtonText.text = "Buy";

                _price = NearUtils.FormatNearAmount(UInt128.Parse(nftSaleInfo.Sale.sale_conditions["near"])).ToString();
                price.text = "Price: " + _price;
            }
            else
            {
                buyImmediatelyView.gameObject.SetActive(false);
                setBidView.gameObject.SetActive(true);
                buyButtonText.text = "Set";

                if (!nftSaleInfo.Sale.sale_conditions.ContainsKey("near"))
                {
                    return;
                }
                
                BidText saleConditionText = Instantiate(Game.AssetRoot.marketplaceAsset.bid, bidContent);
                
                saleConditionText.bid.text = "Sale conditions" + ":  " + NearUtils.FormatNearAmount(UInt128.Parse(_nftSaleInfo.Sale.sale_conditions["near"]));
                
                _bidTexts.Add(saleConditionText);
                if (!nftSaleInfo.Sale.bids.ContainsKey("near"))
                {
                    return;
                }
                
                foreach (Bid saleBid in nftSaleInfo.Sale.bids["near"])
                {
                    BidText bidText = Instantiate(Game.AssetRoot.marketplaceAsset.bid, bidContent);

                    string ownerId = saleBid.owner_id != NearPersistentManager.Instance.GetAccountId() ? saleBid.owner_id : "Your bid";
                    bidText.bid.text = ownerId + ":  " + NearUtils.FormatNearAmount(UInt128.Parse(saleBid.price));
                    
                    _bidTexts.Add(bidText);
                }
            }
        }

        public void BuyCard()
        {
            if (_nftSaleInfo.Sale.is_auction)
            {
                _price = bid.text;
            }
            
            viewInteractor.MarketplaceController.Offer(_nftSaleInfo.NFT.token_id, "near", _price);
        }
    }
}