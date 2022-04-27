using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    public class GoalieInputUI : NftCardInputUI
    {
        [SerializeField] private InputField gloveAndBlocker;
        [SerializeField] private InputField pads;
        [SerializeField] private InputField stand;
        [SerializeField] private InputField stretch;
        [SerializeField] private InputField morale;

        public override void MintCard(Dictionary<string, double> royalties, string url)
        {
            throw new System.NotImplementedException();
        }
    }
}