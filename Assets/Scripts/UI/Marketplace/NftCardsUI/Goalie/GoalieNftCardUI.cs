using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    class GoalieRenderer : ICardRenderer
    {
        private readonly dynamic _cardData;
        
        public GoalieRenderer(dynamic cardData)
        {
            _cardData = cardData;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            GoalieNftCardUI goalie =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.goalieNftCardUI, content);
            
            // TODO insert from CardData

            goalie.Name.text = _cardData["name"].ToString();
            goalie.OwnerId.text = _cardData["ownerId"].ToString();
            goalie.Price.text = _cardData["price"].ToString();

            goalie.Type.text = _cardData["type"].ToString();
            goalie.Position.text = _cardData["position"].ToString();
            goalie.Role.text = _cardData["role"].ToString();

            goalie.GloveAndBlocker.text = _cardData["gloveAndBlocker"].ToString();
            goalie.Pads.text = _cardData["pads"].ToString();
            goalie.Stretch.text = _cardData["stretch"].ToString();
            goalie.Stand.text = _cardData["stand"].ToString();
            goalie.Morale.text = _cardData["morale"].ToString();
            
            return goalie;
        }

        public NftCardDescriptionUI RenderCardDescription(Transform content)
        {
            GoalieDescriptionUI cardDescription =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.goalieDescriptionUI, content);
            
            // TODO insert from CardData

            cardDescription.OwnerId.text = _cardData["ownerId"].ToString();
            cardDescription.Price.text = _cardData["price"].ToString();

            cardDescription.GloveAndBlocker.text = _cardData["gloveAndBlocker"].ToString();
            cardDescription.Pads.text = _cardData["pads"].ToString();
            cardDescription.Stretch.text = _cardData["stretch"].ToString();
            cardDescription.Stand.text = _cardData["stand"].ToString();
            cardDescription.Morale.text = _cardData["morale"].ToString();
            
            return cardDescription;
        }
    }
    
    public class GoalieNftCardUI : NftCardUI
    {
        [SerializeField] private Text type;
        [SerializeField] private Text position;
        [SerializeField] private Text role;
        
        [SerializeField] private Text gloveAndBlocker;
        [SerializeField] private Text pads;
        [SerializeField] private Text stand;
        [SerializeField] private Text stretch;
        [SerializeField] private Text morale;

        public Text Type => type;
        public Text Position => position;
        public Text Role => role;

        public Text GloveAndBlocker => gloveAndBlocker;
        public Text Pads => pads;
        public Text Stand => stand;
        public Text Stretch => stretch;
        public Text Morale => morale;
        
        protected override ICardRenderer GetCardRenderer()
        {
            return new GoalieRenderer(CardData);
        }
    }
}