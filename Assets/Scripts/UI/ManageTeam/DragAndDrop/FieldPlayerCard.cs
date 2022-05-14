using Near.Models;
using Near.Models.Extras;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ManageTeam.DragAndDrop
{
    public class FieldPlayerCard : UIPlayer
    {
        [SerializeField] private Text skating;
        [SerializeField] private Text shooting;
        [SerializeField] private Text strength;
        [SerializeField] private Text iq;
        [SerializeField] private Text morale;
        
        public override void SetData(Metadata metadata)
        {
            playerName.text = metadata.title;
            
            if (!string.IsNullOrEmpty(metadata.media))
            {
                StartCoroutine(Utils.Utils.LoadImage(playerImg, metadata.media));
            }
            
            FieldPlayerExtra goalieExtra =  (FieldPlayerExtra)metadata.extra.GetExtra();
            number.text = goalieExtra.Number.ToString();
            role.text = goalieExtra.Role;
            position.text = goalieExtra.Position;

            skating.text = goalieExtra.Stats.Skating.ToString();
            shooting.text = goalieExtra.Stats.Shooting.ToString();
            strength.text = goalieExtra.Stats.Strength.ToString();
            iq.text = goalieExtra.Stats.IQ.ToString();
            morale.text = goalieExtra.Stats.Morale.ToString();
        }
    }
}