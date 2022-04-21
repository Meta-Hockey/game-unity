using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    class FieldPlayerRenderer : ICardRenderer
    {
        private readonly dynamic _cardData;
        
        public FieldPlayerRenderer(dynamic cardData)
        {
            _cardData = cardData;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            FieldPlayerNftCardUI fieldPlayer =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile, content);
            
            // TODO insert from CardData

            fieldPlayer.Name.text = _cardData["name"].ToString();
            fieldPlayer.OwnerId.text = _cardData["ownerId"].ToString();
            fieldPlayer.Price.text = _cardData["price"].ToString();

            fieldPlayer.Type.text = _cardData["type"].ToString();
            fieldPlayer.Position.text = _cardData["position"].ToString();
            fieldPlayer.Role.text = _cardData["role"].ToString();

            fieldPlayer.Skating.text = _cardData["skating"].ToString();
            fieldPlayer.Shooting.text = _cardData["shooting"].ToString();
            fieldPlayer.Strength.text = _cardData["strength"].ToString();
            fieldPlayer.Iq.text = _cardData["iq"].ToString();
            fieldPlayer.Morale.text = _cardData["morale"].ToString();
            
            return fieldPlayer;
        }

        public NftCardDescriptionUI RenderCardDescription(Transform content)
        {
            FieldPlayerDescriptionUI cardDescription =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardDescription, content);
            
            // TODO insert from CardData

            cardDescription.OwnerId.text = _cardData["ownerId"].ToString();
            cardDescription.Price.text = _cardData["price"].ToString();

            cardDescription.Skating.text = _cardData["skating"].ToString();
            cardDescription.Shooting.text = _cardData["shooting"].ToString();
            cardDescription.Strength.text = _cardData["strength"].ToString();
            cardDescription.Iq.text = _cardData["iq"].ToString();
            cardDescription.Morale.text = _cardData["morale"].ToString();
            
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