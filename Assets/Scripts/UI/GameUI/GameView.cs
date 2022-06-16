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
            controller = new GameController();
            gameId = await controller.GetGameId();
            await controller.GenerateEvents(0, gameId); 
            // StartCoroutine(ShowEvents());
        }

        private IEnumerator ShowEvents()
        {
            while (true)
            {
                if (gameId != -1)
                {
                    controller.GenerateEvents(0, gameId);
                }
                
                yield return new WaitForSeconds(10);
            }
        }
    }
}