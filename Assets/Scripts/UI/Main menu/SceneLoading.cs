using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneLoading : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage image;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(AnimationPlaying());
    }

    private IEnumerator AnimationPlaying()
    {
        float deltaScale = 0.02f;
        
        while (rectTransform.localScale.x < 1)
        {
            float newScale = rectTransform.localScale.x + deltaScale;
            
            rectTransform.localScale = new Vector3(newScale, newScale, 1);

            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadSceneAsync("MainMenu");
    }
}
