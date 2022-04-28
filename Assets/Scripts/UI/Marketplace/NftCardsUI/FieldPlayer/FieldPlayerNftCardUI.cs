using Near.Models;
using Near.Models.Extras;
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
            
            fieldPlayer.gameObject.SetActive(true);
            fieldPlayer.LoadImage(_nftSaleInfo.NFT.metadata.media);
            fieldPlayer.Name.text = _nftSaleInfo.NFT.metadata.title;
            fieldPlayer.OwnerId.text = _nftSaleInfo.NFT.owner_id;
            
            // TODO: Card price
            fieldPlayer.Price.text = "Price: " + _nftSaleInfo.Sale.sale_conditions["Near"];

            FieldPlayerExtra extra = (FieldPlayerExtra)_nftSaleInfo.NFT.metadata.extra.GetExtra();

            // fieldPlayer.Image = _cardData.metadata.media;
            fieldPlayer.Type.text = extra.Type;
            fieldPlayer.Position.text = extra.Position;
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

            cardDescription.OwnerId.text = _nftSaleInfo.NFT.owner_id;
            cardDescription.Price.text = "Price: " + _nftSaleInfo.Sale.sale_conditions["NEAR"];
            // TODO: Card price

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
        [SerializeField] private Text type;
        [SerializeField] private Text position;
        [SerializeField] private Text role;
        
        [SerializeField] private Text skating;
        [SerializeField] private Text shooting;
        [SerializeField] private Text strength;
        [SerializeField] private Text iq;
        [SerializeField] private Text morale;

        public Text Type => type;
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