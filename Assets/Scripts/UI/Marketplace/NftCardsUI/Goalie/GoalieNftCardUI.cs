using Near.Models;
using Near.Models.Extras;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    class GoalieRenderer : ICardRenderer
    {
        private readonly NFT _cardData;
        private readonly MonoBehaviour _monoBehaviour;
        
        public GoalieRenderer(NFT cardData)
        {
            _cardData = cardData;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            GoalieNftCardUI goalie =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.goalieNftCardUI, content);
            
            goalie.LoadImage(_cardData.metadata.media);
            goalie.Name.text = _cardData.metadata.title;
            goalie.OwnerId.text = _cardData.owner_id;
            
            // TODO: Card price

            GoalieExtra extra = (GoalieExtra)_cardData.metadata.extra.GetExtra();
            
            goalie.Type.text = extra.Type;
            goalie.Position.text = extra.Position;
            goalie.Role.text = extra.Role;

            goalie.GloveAndBlocker.text = extra.Stats.GloveAndBlocker.ToString();
            goalie.Pads.text = extra.Stats.Pads.ToString();
            goalie.Stretch.text = extra.Stats.Stretch.ToString();
            goalie.Stand.text = extra.Stats.Stand.ToString();
            goalie.Morale.text = extra.Stats.Morale.ToString();
            
            return goalie;
        }

        public NftCardDescriptionUI RenderCardDescription(Transform content)
        {
            GoalieDescriptionUI cardDescription =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.goalieDescriptionUI, content);
            
            // TODO insert from CardData

            cardDescription.OwnerId.text = _cardData.owner_id;

            GoalieExtra extra = (GoalieExtra)_cardData.metadata.extra.GetExtra();

            cardDescription.GloveAndBlocker.text = extra.Stats.GloveAndBlocker.ToString();
            cardDescription.Pads.text = extra.Stats.Pads.ToString();
            cardDescription.Stretch.text = extra.Stats.Stretch.ToString();
            cardDescription.Stand.text = extra.Stats.Stand.ToString();
            cardDescription.Morale.text = extra.Stats.Morale.ToString();
            
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