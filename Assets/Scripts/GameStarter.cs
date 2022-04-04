using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] 
    private AssetRoot assetRoot;
    
    private void Awake()
    {   
        SceneManager.LoadScene(assetRoot.mainMenuUIScene.name, LoadSceneMode.Additive);
    }
}