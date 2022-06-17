using UI.GameUI.EventsUI;
using UnityEngine;

namespace UI.GameUI
{
    [CreateAssetMenu(menuName = "Assets/Asset Game", fileName = "Asset Game")]
    public class GameAsset : ScriptableObject
    {
        public DefaultEvent defaultEvent;
        public GoalEvent goalEvent;
        public OpponentEvent opponentEvent;
        public OwnEvent ownEvent;
    }
}