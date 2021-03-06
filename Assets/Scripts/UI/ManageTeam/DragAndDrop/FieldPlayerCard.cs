using Near.Models.Extras;
using Near.Models.Team.Team;
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
        
        public override void SetData(NFTMetadata nftMetadata)
        {
            CardData = nftMetadata;
            
            playerName.text = nftMetadata.Metadata.title;
            
            if (!string.IsNullOrEmpty(nftMetadata.Metadata.media))
            {
                StartCoroutine(Utils.Utils.LoadImage(playerImg, nftMetadata.Metadata.media));
            }
            else
            {
                silverStroke.gameObject.SetActive(true);
            }
            
            FieldPlayerExtra goalieExtra =  (FieldPlayerExtra)nftMetadata.Metadata.extra.GetExtra();
            number.text = goalieExtra.Number.ToString();
            role.text = goalieExtra.Role;
            position.text = Utils.Utils.ConvertPosition(goalieExtra.Position);

            skating.text = goalieExtra.Stats.Skating.ToString();
            shooting.text = goalieExtra.Stats.Shooting.ToString();
            strength.text = goalieExtra.Stats.Strength.ToString();
            iq.text = goalieExtra.Stats.IQ.ToString();
            morale.text = goalieExtra.Stats.Morale.ToString();
        }
    }
}