using UnityEngine;
using UnityEngine.UI;
using Event = Near.Models.Game.Event;

namespace UI.GameUI.EventsUI
{
    public class OwnEvent : MonoBehaviour
    {
        public Text eventText;
        public Text ownNumberText;
        public Text opponentPlayerNumber;

        public void ShowEvent(Event data)
        {
            eventText.text = data.action;
            ownNumberText.text = data.player_with_puck.number.ToString();

            switch (data.action)
            {
                case "PuckLose":
                case "Pass":
                case "Move":
                    return;
                case "Shot":
                    opponentPlayerNumber.text = data.my_team.goalie.number.ToString();
                    return;
            }
            
            string pos = data.player_with_puck.position;
            opponentPlayerNumber.text = data.my_team.five.field_players[pos].number.ToString();
        }
    }
}