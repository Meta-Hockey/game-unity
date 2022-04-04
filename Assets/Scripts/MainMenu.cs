using Assets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] 
    private AssetRoot _assetRoot;
    
    private void Awake()
    {   
        SceneManager.LoadScene(_assetRoot.mainMenuScene.name, LoadSceneMode.Additive);
    }
}
