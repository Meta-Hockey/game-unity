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
    }
}