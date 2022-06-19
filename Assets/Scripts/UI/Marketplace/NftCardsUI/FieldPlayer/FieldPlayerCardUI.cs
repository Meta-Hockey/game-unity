using Near.Models;
using Near.Models.Extras;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    public class FieldPlayerCardUI : CardUI
    {
        [SerializeField] private Text position;
        [SerializeField] private Text number;
        [SerializeField] private Text role;

        [SerializeField] private Text skating;
        [SerializeField] private Text shooting;
        [SerializeField] private Text strength;
        [SerializeField] private Text iQ;
        [SerializeField] private Text morale;

        public Text Position => position;
        public Text Number => number;
        public Text Role => role;
        
        public Text Skating => skating;
        public Text Shooting => shooting;
        public Text Strength => strength;
        public Text IQ => iQ;
        public Text Morale => morale;


        public override void SetData(NFTSaleInfo nftSaleInfo)
        {
            CardName.text = nftSaleInfo.NFT.metadata.title;
            StartCoroutine(Utils.Utils.LoadImage(Image, nftSaleInfo.NFT.metadata.media));
            
            FieldPlayerExtra extra = (FieldPlayerExtra)nftSaleInfo.NFT.metadata.extra.GetExtra();

            Number.text = extra.Number.ToString();
            Position.text = Utils.Utils.ConvertPosition(extra.Position);
            Role.text = extra.Role;

            Skating.text = extra.Stats.Skating.ToString();
            Shooting.text = extra.Stats.Shooting.ToString();
            Strength.text = extra.Stats.Strength.ToString();
            IQ.text = extra.Stats.IQ.ToString();
            Morale.text = extra.Stats.Morale.ToString();
        }
    }
}