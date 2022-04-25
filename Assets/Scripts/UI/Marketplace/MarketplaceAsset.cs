using UI.Marketplace.NftCardsUI.FieldPlayer;
using UI.Marketplace.NftCardsUI.Goalie;
using UnityEditor;
using UnityEngine;

namespace UI.Marketplace
{
    [CreateAssetMenu(menuName = "Assets/Asset Marketplace", fileName = "Asset Marketplace")]
    public class MarketplaceAsset : ScriptableObject
    {
        public SceneAsset marketplaceScene;
        
        public FieldPlayerNftCardUI fieldPlayerCardTile;
        public FieldPlayerDescriptionUI fieldPlayerCardDescription;
        public FieldPlayerInputUI fieldPlayerInputUI;

        public GoalieNftCardUI goalieNftCardUI;
        public GoalieDescriptionUI goalieDescriptionUI;
        public GoalieInputUI goalieInputUI;
    }
}