using System.Collections;
using System.Collections.Generic;
using Runtime;
using UI.GameUI.Events;
using UnityEngine;
using UnityEngine.UI;
using Event = Near.Models.Game.Event;

namespace UI.GameUI
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Transform eventsContent;
        [SerializeField] private ScrollRect scrollView;

        private bool isEndOfGame;
        private int gameId;
        private int numberOfRenderedEvents;
        
        private GameController controller;
        private List<Event> events;
        
        private async void Start()
        {
            controller = new GameController();
            gameId = await controller.GetGameId();
            
            numberOfRenderedEvents = 0;
            isEndOfGame = false;
            
            if (gameId != -1)
            {
                StartCoroutine(GenerateEvents());
            }
        }

        private IEnumerator GenerateEvents()
        {
            while (!isEndOfGame)
            {
                controller.GenerateEvents(gameId);
                RenderEvents();
                
                yield return new WaitForSeconds(2);
            }
        }

        private void RenderEvents()
        {
            if (numberOfRenderedEvents == controller.NumberOfGeneratedEvents)
            {
                return;
            }
            
            int numberOfGeneratedEvents = controller.NumberOfGeneratedEvents;
            for (int i = numberOfRenderedEvents; i < numberOfGeneratedEvents; i++)
            {
                Event data = controller.Events[i];

                int myId = data.my_team.goalie.user_id;
                

                switch (data.action)
                {
                    case "Overtime":
                    case "FaceOff":
                    case "EndOfPeriod":
                    case "GameFinished":
                    case "StartGame":
                        OtherEvent otherEvent = Instantiate(Game.AssetRoot.gameAsset.otherEvent, eventsContent);
                        otherEvent.transform.SetAsLastSibling();
                        
                        otherEvent.textAction.text = data.action;
                        
                        continue;
                    case "Goal":
                        GoalEvent goalEvent = Instantiate(Game.AssetRoot.gameAsset.goalEvent, eventsContent);
                       
                        goalEvent.transform.SetAsLastSibling();
                        goalEvent.textColor = myId != data.player_with_puck.user_id ? Color.red : Color.blue;

                        continue;
                }
                
                if (data.player_with_puck.user_id != myId)
                {
                    OpponentEvent opponentEvent = Instantiate(Game.AssetRoot.gameAsset.opponentEvent, eventsContent);
                   
                    opponentEvent.transform.SetAsLastSibling(); 
                    opponentEvent.textAction.text = data.action;
                }
                else
                {
                    OwnEvent ownEvent = Instantiate(Game.AssetRoot.gameAsset.ownEvent, eventsContent);
                    
                    ownEvent.transform.SetAsLastSibling();
                    ownEvent.textAction.text = data.action;
                }
            }

            scrollView.verticalNormalizedPosition = 0;
            numberOfRenderedEvents = numberOfGeneratedEvents;
        }

        private void UpdateStats(Event data)
        {
        }
    }
}