using UnityEditor;
using UnityEngine;

namespace UI.Marketplace
{
    
    [CreateAssetMenu(menuName = "Assets/Asset Marketplace", fileName = "Asset Marketplace")]
    public class MarketplaceAsset : ScriptableObject
    {
        public SceneAsset marketplaceScene;
        
        public NftCardInfoUI nftCardInfoUI;
    }
}