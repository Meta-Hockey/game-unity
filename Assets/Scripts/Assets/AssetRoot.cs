using UI.Marketplace;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class AssetRoot : ScriptableObject
    {
        public SceneAsset signInUIScene;
        public SceneAsset mainMenuUIScene;
        
        public SceneAsset betsScene;
        public SceneAsset betsUIScene;

        public MarketplaceAsset marketplaceAsset;
    }
}