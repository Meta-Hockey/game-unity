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
        [SerializeField] private UIPopupResult resultView;
        
        [SerializeField] private Text ownScoreText;
        [SerializeField] private Text opponentScoreText;
        [SerializeField] private Text periodText;
        
        [SerializeField] private Transform eventsContent;
        [SerializeField] private ScrollRect scrollView;

        private bool isEndOfGame;
        private int gameId;
        private int numberOfRenderedEvents;
        
        private GameController controller;
        private List<Event> events;

        private bool isRenders;
        
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

                if (!isRenders)
                {
                    StartCoroutine(RenderEvents());
                }
                
                yield return new WaitForSeconds(1);
            }
        }

        private IEnumerator RenderEvents()
        {
            if (numberOfRenderedEvents == controller.NumberOfGeneratedEvents)
            {
                yield break;
            }

            isRenders = true;
            
            int numberOfGeneratedEvents = controller.NumberOfGeneratedEvents;
            for (int i = numberOfRenderedEvents; i < numberOfGeneratedEvents; i++)
            {
                yield return new WaitForSeconds(1);
                scrollView.verticalNormalizedPosition = 0;
                
                Event data = controller.Events[i];

                int myId = data.my_team.goalie.user_id;

                uint ownScore = data.my_team.score;
                uint opponentScore = data.opponent_team.score;

                ownScoreText.text = ownScore < 10 ? "0" + ownScore : ownScore.ToString();
                opponentScoreText.text = opponentScore < 10 ? "0" + opponentScore : opponentScore.ToString();
                
                switch (data.action)
                {
                    case "Overtime":
                    case "FaceOff":
                    case "EndOfPeriod":
                    case "StartGame":
                        OtherEvent otherEvent = Instantiate(Game.AssetRoot.gameAsset.otherEvent, eventsContent);
                        otherEvent.transform.SetAsLastSibling();
                        
                        otherEvent.textAction.text = data.action;
                        
                        continue;
                    case "Goal":
                        GoalEvent goalEvent = Instantiate(Game.AssetRoot.gameAsset.goalEvent, eventsContent);
                       
                        goalEvent.transform.SetAsLastSibling();
                        goalEvent.text.color = myId != data.player_with_puck.user_id ? Color.red : Color.blue;

                        continue;
                    case "GameFinish":
                        isEndOfGame = true;
                        resultView.ShowResult(ownScore, opponentScore);
                        
                        yield break;
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

            isRenders = false;
        }

        private void UpdateStats(Event data)
        {
        }
    }
}