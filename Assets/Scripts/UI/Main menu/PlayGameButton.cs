using System;
using Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Main_menu
{
    public class PlayGameButton : MonoBehaviour
    {
        public void LoadBetsScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(Game.AssetRoot.betsScene.name);
            operation.completed += LoadUI;
        }

        private void LoadUI(AsyncOperation operation)
        {
            if (!operation.isDone)
            {
                throw new Exception("Can't load scene");
            }
            
            SceneManager.LoadScene(Game.AssetRoot.betsUIScene.name, LoadSceneMode.Additive);
        }
    }
}