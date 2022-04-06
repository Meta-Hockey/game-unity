using Assets;
using UnityEngine;

namespace Runtime
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private AssetRoot assetRoot;
        
        private void Awake()
        {
            Game.SetAssetRoot(assetRoot);
        }
    }
}