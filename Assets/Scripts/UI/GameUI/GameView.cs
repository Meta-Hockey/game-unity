using System.Collections;
using System.Collections.Generic;
using Runtime;
using UI.GameUI.EventsUI;
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
                
                yield return new WaitForSeconds(2);
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
                    case "EndOfPeriod":
                        DefaultEvent endOfPer = Instantiate(Game.AssetRoot.gameAsset.defaultEvent, eventsContent); 
                        endOfPer.ShowEvent(data);
                        ChangePeriod();
                        
                        continue;
                    case "Overtime":
                    case "FaceOff":
                    case "StartGame":
                        DefaultEvent defaultEvent = Instantiate(Game.AssetRoot.gameAsset.defaultEvent, eventsContent);
                        defaultEvent.ShowEvent(data);
                        
                        continue;
                    case "Goal":
                        GoalEvent goalEvent = Instantiate(Game.AssetRoot.gameAsset.goalEvent, eventsContent);
                        goalEvent.ShowEvent(data); 
                        
                        continue;
                    case "GameFinish":
                        DefaultEvent gameFinish = Instantiate(Game.AssetRoot.gameAsset.defaultEvent, eventsContent); 
                        gameFinish.ShowEvent(data); 
                        
                        isEndOfGame = true;
                        resultView.ShowResult(ownScore, opponentScore);
                        
                        yield break;
                }
                
                if (data.player_with_puck.user_id != myId)
                {
                    OpponentEvent opponentEvent = Instantiate(Game.AssetRoot.gameAsset.opponentEvent, eventsContent);
                    opponentEvent.ShowEvent(data);
                }
                else
                {
                    OwnEvent ownEvent = Instantiate(Game.AssetRoot.gameAsset.ownEvent, eventsContent);
                    ownEvent.ShowEvent(data);
                }
            }

            scrollView.verticalNormalizedPosition = 0;
            numberOfRenderedEvents = numberOfGeneratedEvents;

            isRenders = false;
        }

        private void ChangePeriod()
        {
            int period = int.Parse(periodText.text);
            if (period < 3)
            {
                periodText.text = (period + 1).ToString();
            }
        }
        
        private void UpdateStats(Event data)
        {
        }
    }
}