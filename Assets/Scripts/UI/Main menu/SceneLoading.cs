using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    [Header("Loading scene")]
    public int sceneID;
    [Header("Loading scene")]
    public Image loadingImg;
    public Text progressText;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            progressText.text = string.Format("{0:0}", progress * 100);
            yield return null;

        }
    }
    
}
