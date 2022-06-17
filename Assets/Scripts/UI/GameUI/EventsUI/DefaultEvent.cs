using UnityEngine;
using UnityEngine.UI;
using Event = Near.Models.Game.Event;

namespace UI.GameUI.EventsUI
{
    public class DefaultEvent : MonoBehaviour
    {
        public Text eventText;

        public void ShowEvent(Event data)
        {
            transform.SetAsLastSibling();

            eventText.text = data.action;
        }
    }
}