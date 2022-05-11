using UI.Marketplace;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class AssetRoot : ScriptableObject
    {
        public MarketplaceAsset marketplaceAsset;
    }
}