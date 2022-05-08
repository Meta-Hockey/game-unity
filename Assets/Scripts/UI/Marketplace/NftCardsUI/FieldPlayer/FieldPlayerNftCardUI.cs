using Near;
using Near.Models;
using Near.Models.Extras;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    class FieldPlayerRenderer : ICardRenderer
    {
        private readonly NFTSaleInfo _nftSaleInfo;
        
        public FieldPlayerRenderer(NFTSaleInfo nftSaleInfo)
        {
            _nftSaleInfo = nftSaleInfo;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            FieldPlayerNftCardUI fieldPlayer =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile, content);
            
            fieldPlayer.Name.text = _nftSaleInfo.NFT.metadata.title;
            fieldPlayer.OwnerId.text = _nftSaleInfo.NFT.owner_id != NearPersistentManager.Instance.GetAccountId() 
                ? "Owner: " + _nftSaleInfo.NFT.owner_id : "You are the owner";
            
            if (_nftSaleInfo.Sale != null && _nftSaleInfo.Sale.is_auction)
            {
                fieldPlayer.Price.text = "Auction";
            }
            else if (_nftSaleInfo.Sale != null && _nftSaleInfo.Sale.sale_conditions.ContainsKey("near"))
            {
                fieldPlayer.Price.text = "Cost: " + NearUtils.FormatNearAmount(UInt128.Parse(_nftSaleInfo.Sale.sale_conditions["near"]));
            }
            else
            {
                fieldPlayer.Price.text = "Not for sale";
            }

            FieldPlayerExtra extra = (FieldPlayerExtra)_nftSaleInfo.NFT.metadata.extra.GetExtra();

            fieldPlayer.LoadImage(_nftSaleInfo.NFT.metadata.media);
            fieldPlayer.Number.text = extra.Number.ToString();
            fieldPlayer.Position.text = Utils.Utils.ConvertPosition(extra.Position);
            fieldPlayer.Role.text = extra.Role;
            
            fieldPlayer.Skating.text = extra.Stats.Skating.ToString();
            fieldPlayer.Shooting.text = extra.Stats.Shooting.ToString();
            fieldPlayer.Strength.text = extra.Stats.Strength.ToString();
            fieldPlayer.Iq.text = extra.Stats.IQ.ToString();
            fieldPlayer.Morale.text = extra.Stats.Morale.ToString();
            
            return fieldPlayer;
        }

        public NftCardDescriptionUI RenderCardDescription(Transform content)
        {
            FieldPlayerDescriptionUI cardDescription =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardDescription, content);
            
            FieldPlayerExtra extra = (FieldPlayerExtra)_nftSaleInfo.NFT.metadata.extra.GetExtra();

            cardDescription.Skating.text = extra.Stats.Skating.ToString();
            cardDescription.Shooting.text = extra.Stats.Shooting.ToString();
            cardDescription.Strength.text = extra.Stats.Shooting.ToString();
            cardDescription.Iq.text = extra.Stats.IQ.ToString();
            cardDescription.Morale.text = extra.Stats.Morale.ToString();
            
            return cardDescription;
        }
    }
    
    public class FieldPlayerNftCardUI : NftCardUI
    {
        [SerializeField] private Text number;
        [SerializeField] private Text position;
        [SerializeField] private Text role;
        
        [SerializeField] private Text skating;
        [SerializeField] private Text shooting;
        [SerializeField] private Text strength;
        [SerializeField] private Text iq;
        [SerializeField] private Text morale;

        public Text Number => number;
        public Text Position => position;
        public Text Role => role;

        public Text Skating => skating;
        public Text Shooting => shooting;
        public Text Strength => strength;
        public Text Iq => iq;
        public Text Morale => morale;
        
        protected override ICardRenderer GetCardRenderer()
        {
            return new FieldPlayerRenderer(NftSaleInfo);
        }
    }
}