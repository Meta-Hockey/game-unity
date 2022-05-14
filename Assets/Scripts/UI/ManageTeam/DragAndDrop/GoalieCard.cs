using Near.Models;
using Near.Models.Extras;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ManageTeam.DragAndDrop
{
    public class GoalieCard : UIPlayer
    {
        [SerializeField] private Text gloveAndBlocker;
        [SerializeField] private Text pads;
        [SerializeField] private Text stand;
        [SerializeField] private Text stretch;
        [SerializeField] private Text morale;
        
        public override void SetData(Metadata metadata)
        {
            playerName.text = metadata.title;

            if (!string.IsNullOrEmpty(metadata.media))
            {
                StartCoroutine(Utils.Utils.LoadImage(playerImg, metadata.media));
            }
            else
            {
                silverStroke.gameObject.SetActive(true);
            }

            GoalieExtra goalieExtra =  (GoalieExtra)metadata.extra.GetExtra();
            number.text = goalieExtra.Number.ToString();
            role.text = goalieExtra.Role;
            position.text = goalieExtra.Position;

            gloveAndBlocker.text = goalieExtra.Stats.GloveAndBlocker.ToString();
            pads.text = goalieExtra.Stats.Pads.ToString();
            stand.text = goalieExtra.Stats.Stand.ToString();
            stretch.text = goalieExtra.Stats.Stretch.ToString();
            morale.text = goalieExtra.Stats.Morale.ToString();
        }
    }
}