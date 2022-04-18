using System;
using Assets;
using Near;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Runtime
{
    public static class Game
    {
        private static AssetRoot _assetRoot;
        private static Runner _runner;
        
        public static AssetRoot AssetRoot => _assetRoot;
        
        public static void SetAssetRoot(AssetRoot assetRoot)
        {
            _assetRoot = assetRoot;
        }
        
        public static void LoadMarketplace()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(_assetRoot.marketplaceScene.name);
            operation.completed += StartPlayer;
        }

        private static void StartPlayer(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception("Can't load scene");
            }
            
            _runner = Object.FindObjectOfType<Runner>();
            _runner.StartRunning();
        }

        public static void LoadMainMenu()
        {
            if (NearPersistentManager.Instance.WalletAccount.IsSignedIn())
            {
                SceneManager.LoadScene(_assetRoot.mainMenuUIScene.name, LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(_assetRoot.signInUIScene.name, LoadSceneMode.Additive);
            }
        }
    }
}