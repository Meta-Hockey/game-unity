using System.Collections;
using UnityEngine;

namespace UI.GameUI
{
    public class GameView : MonoBehaviour
    {
        private int gameId = -1;
        private int numberOfRenderedEvents;
        private GameController controller;
        
        private async void Start()
        {
            gameId = await controller.GetGameId();
            controller = new GameController();
            
            StartCoroutine(ShowEvents());
        }

        private IEnumerator ShowEvents()
        {
            while (true)
            {
                if (gameId == -1)
                {
                    
                }
                
                yield return new WaitForSeconds(1);
            }
        }
    }
}