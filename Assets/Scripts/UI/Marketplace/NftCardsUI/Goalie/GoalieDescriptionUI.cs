using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    public class GoalieDescriptionUI : NftCardDescriptionUI
    {
        [SerializeField] private Text gloveAndBlocker;
        [SerializeField] private Text pads;
        [SerializeField] private Text stand;
        [SerializeField] private Text stretch;
        [SerializeField] private Text morale;
        
        public Text GloveAndBlocker => gloveAndBlocker;
        public Text Pads => pads;
        public Text Stand => stand;
        public Text Stretch => stretch;
        public Text Morale => morale;
    }
}