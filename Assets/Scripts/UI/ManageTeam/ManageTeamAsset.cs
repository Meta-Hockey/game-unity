using UI.ManageTeam.DragAndDrop;
using UnityEngine;

namespace UI.ManageTeam
{
    [CreateAssetMenu(menuName = "Assets/Asset ManageTeam", fileName = "Asset ManageTeam")]
    public class ManageTeamAsset : ScriptableObject
    {
        public UIPlayer fieldPlayer;
        public UIPlayer goalie;
    }
}