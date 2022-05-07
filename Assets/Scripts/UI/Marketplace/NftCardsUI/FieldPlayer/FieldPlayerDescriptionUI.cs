using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    public class FieldPlayerDescriptionUI : NftCardDescriptionUI
    {
        [SerializeField] private Text number;
        [SerializeField] private Text position;
        [SerializeField] private Text role;

        [SerializeField] private Text skating;
        [SerializeField] private Text shooting;
        [SerializeField] private Text strength;
        [SerializeField] private Text iq;
        [SerializeField] private Text morale;

        public Text Number => number;
        public Text Position => position;
        public Text Role => role;
        
        public Text Skating => skating;
        public Text Shooting => shooting;
        public Text Strength => strength;
        public Text Iq => iq;
        public Text Morale => morale;
    }
}