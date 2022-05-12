using Near;
using Near.Models;
using Near.Models.Extras;
using NearClientUnity.Utilities;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    class GoalieRenderer : ICardRenderer
    {
        private readonly NFTSaleInfo _nftSaleInfo;
        
        public GoalieRenderer(NFTSaleInfo nftSaleInfo)
        {
            _nftSaleInfo = nftSaleInfo;
        }
        
        public NftCardUI RenderCardTile(Transform content)
        {
            GoalieNftCardUI goalie =
                Object.Instantiate(Game.AssetRoot.marketplaceAsset.goalieNftCardUI, content);

            goalie.Name.text = _nftSaleInfo.NFT.metadata.title;
            
            goalie.OwnerId.text = _nftSaleInfo.NFT.owner_id != NearPersistentManager.Instance.GetAccountId() ? "Owner: " + _nftSaleInfo.NFT.owner_id : "You are the owner";
            
            
            if (_nftSaleInfo.Sale != null && _nftSaleInfo.Sale.is_auction)
            {
                goalie.Cost.text = "Auction";
            }
            else if (_nftSaleInfo.Sale != null && _nftSaleInfo.Sale.sale_conditions.ContainsKey("near"))
            {
                goalie.Cost.text = "Price: " + NearUtils.FormatNearAmount(UInt128.Parse(_nftSaleInfo.Sale.sale_conditions["near"]));
            }
            else
            {
                goalie.Price.gameObject.SetActive(false);
            }
            
            
            GoalieExtra extra = (GoalieExtra)_nftSaleInfo.NFT.metadata.extra.GetExtra();
            
            goalie.Number.text = extra.Number.ToString();
            goalie.LoadImage(_nftSaleInfo.NFT.metadata.media);
            goalie.Position.text = Utils.Utils.ConvertPosition(extra.Position);
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
            
            cardDescription.OwnerId.text =  _nftSaleInfo.NFT.owner_id != NearPersistentManager.Instance.GetAccountId() 
                ? "Owner: " + _nftSaleInfo.NFT.owner_id : "You are the owner";

            cardDescription.Name.text = _nftSaleInfo.NFT.metadata.title;
            
            GoalieExtra extra = (GoalieExtra)_nftSaleInfo.NFT.metadata.extra.GetExtra();

            cardDescription.Name.text = _nftSaleInfo.NFT.metadata.title;
            cardDescription.Position.text = "Position: " + extra.Position;
            cardDescription.Role.text = "Role: " + extra.Role;
            
            cardDescription.GloveAndBlocker.text = "agility: " +  extra.Stats.GloveAndBlocker;
            cardDescription.Pads.text = "pads: " + extra.Stats.Pads;
            cardDescription.Stretch.text = "stretch: " + extra.Stats.Stretch;
            cardDescription.Stand.text = "stand: " + extra.Stats.Stand;
            cardDescription.Morale.text = "morale: " + extra.Stats.Morale;
            
            return cardDescription;
        }
    }
    
    public class GoalieNftCardUI : NftCardUI
    {
        [SerializeField] private Text number;
        [SerializeField] private Text position;
        [SerializeField] private Text role;
        
        [SerializeField] private Text gloveAndBlocker;
        [SerializeField] private Text pads;
        [SerializeField] private Text stand;
        [SerializeField] private Text stretch;
        [SerializeField] private Text morale;

        public Text Number => number;
        public Text Position => position;
        public Text Role => role;

        public Text GloveAndBlocker => gloveAndBlocker;
        public Text Pads => pads;
        public Text Stand => stand;
        public Text Stretch => stretch;
        public Text Morale => morale;
        
        protected override ICardRenderer GetCardRenderer()
        {
            return new GoalieRenderer(NftSaleInfo);
        }
    }
}