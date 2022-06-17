using UnityEngine;
using UnityEngine.UI;
using Event = Near.Models.Game.Event;

namespace UI.GameUI.EventsUI
{
    public class GoalEvent : MonoBehaviour
    {
        public Text eventText;
        
        public void ShowEvent(Event data)
        {
            transform.SetAsLastSibling();

            eventText.color = data.player_with_puck.user_id != data.my_team.goalie.user_id ? Color.red : Color.blue;
        }
    }
}