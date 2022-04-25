using Near.Models;
using Near.Models.Extras;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    class FieldPlayerRenderer : ICardRenderer
    {
        private readonly NFT _cardData;
        
        public FieldPlayerRenderer(NFT cardData)
        {
            _cardData = cardData;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            FieldPlayerNftCardUI fieldPlayer =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile, content);
            
            fieldPlayer.Name.text = _cardData.metadata.title;
            fieldPlayer.OwnerId.text = _cardData.owner_id;
            
            // TODO: Card price

            FieldPlayerExtra extra = (FieldPlayerExtra)_cardData.metadata.extra.GetExtra();
            
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
            
            FieldPlayerExtra extra = (FieldPlayerExtra)_cardData.metadata.extra.GetExtra();

            cardDescription.OwnerId.text = _cardData.owner_id;

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
            return new FieldPlayerRenderer(CardData);
        }
    }
}