using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utils
{
    public class Utils : MonoBehaviour
    {
        public static IEnumerator LoadImage(Image image, string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            
            yield return request.SendWebRequest();
            
            if (!request.isDone)
            {
                Debug.Log(request.error);
            }
            else
            {
                Texture texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                image.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }
    }
}