using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    public class FieldPlayerInputUI : NftCardInputUI
    {
        [SerializeField] private InputField iQ;
        [SerializeField] private InputField skating;
        [SerializeField] private InputField shooting;
        [SerializeField] private InputField strength;
        [SerializeField] private InputField morale;

        public override void MintCard()
        {
            
        }
    }
}