using Assets;

namespace Runtime
{
    public static class Game
    {
        private static AssetRoot _sAssetRoot;

        public static AssetRoot AssetRoot => _sAssetRoot;


        public static void SetAssetRoot(AssetRoot assetRoot)
        {
            _sAssetRoot = assetRoot;
        }
    }
}