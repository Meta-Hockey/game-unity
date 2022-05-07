using UI.Marketplace;
using UnityEditor;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class AssetRoot : ScriptableObject
    {
        public SceneAsset mainMenuScene;
        public SceneAsset betsScene;
        public SceneAsset betsUIScene;
        public SceneAsset manageTeamScene;
        public MarketplaceAsset marketplaceAsset;
    }
}