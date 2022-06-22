using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Utils
{
    public static class Utils 
    {
        public static IEnumerator LoadImage(Image image, string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            try
            {
                Texture texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
                image.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            catch
            {
                Debug.Log("Invalid link");
            }
        }

        public static string ConvertPosition(string position)
        {
            return position switch
            {
                "LeftWing" => "LW",
                "RightWing" => "RW",
                "Center" => "C",
                "LeftDefender" => "LD",
                "RightDefender" => "RD",
                "GoaliePos" => "G",
                "Goalie" => "G",
                _ => position
            };
        }

        public static int GetSliderValueIceTimePriority(string iceTimePriority)
        {
            return iceTimePriority switch
            {
                "SuperLowPriority" => 1,
                "LowPriority" => 2,
                "Normal" => 3,
                "HighPriority" => 4,
                "SuperHighPriority" => 5,
            };
        }

        public static string GetIceTimePriority(int value)
        {
            return value switch
            {
                1 => "SuperLowPriority",
                2 => "LowPriority",
                3 => "Normal",
                4 => "HighPriority",
                5 => "SuperHighPriority",
            };
        }

        public static int GetFieldPlayerPositionId(string position)
        {
            return position switch
            {
                "LeftWing" => 0,
                "Center" => 1,
                "RightWing" => 2,
                "LeftDefender" => 3,
                "RightDefender" => 4,
            };
        }
    }
}