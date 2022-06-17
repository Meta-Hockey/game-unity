using UI.GameUI;
using UI.Main_menu;
using UI.ManageTeam;
using UI.Marketplace;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu(menuName = "Assets/Asset Root", fileName = "Asset Root")]
    public class AssetRoot : ScriptableObject
    {
        public GameAsset gameAsset;
        public MainMenuAsset mainMenuAsset;
        public MarketplaceAsset marketplaceAsset;
        public ManageTeamAsset manageTeamAsset;
    }
}