using Assets;
using UnityEngine.SceneManagement;

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

        public static void LoadMainMenu()
        {
            if (NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                SceneManager.LoadScene(_sAssetRoot.mainMenuUIScene.name, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(_sAssetRoot.signInUIScene.name, LoadSceneMode.Additive);
            }
        }
    }
}