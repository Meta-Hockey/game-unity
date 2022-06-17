using UI.GameUI.Events;
using UnityEngine;
using Event = Near.Models.Game.Event;

namespace UI.GameUI
{
    [CreateAssetMenu(menuName = "Assets/Asset Game", fileName = "Asset Game")]
    public class GameAsset : ScriptableObject
    {
        public OtherEvent otherEvent;
        public GoalEvent goalEvent;
        public OpponentEvent opponentEvent;
        public OwnEvent ownEvent;
    }
}