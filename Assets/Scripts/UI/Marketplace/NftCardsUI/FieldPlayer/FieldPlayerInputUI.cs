using System.Collections.Generic;
using System.Dynamic;
using Near.MarketplaceContract.ContractMethods;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    public class FieldPlayerInputUI : NftCardInputUI
    {
        private string _position;
        private string _role;

        [SerializeField] private Transform defenderRoles;
        [SerializeField] private Transform forwardRoles;
        
        [SerializeField] private List<RolesButton> rolesButtons;
        [SerializeField] private Toggle hand;
        
        [SerializeField] private InputField number;
        
        [SerializeField] private InputField iQ;
        [SerializeField] private InputField skating;
        [SerializeField] private InputField shooting;
        [SerializeField] private InputField strength;
        [SerializeField] private InputField morale;

        public override void SetRole(string role, RolesButton activeButton)
        {
            foreach (RolesButton button in rolesButtons)
            {
                button.text.color = Color.white;
                button.image.sprite = button.defaultSprite;
            }

            activeButton.text.color = Color.black;
            activeButton.image.sprite = activeButton.activeSprite;

            _role = role;
        }

        public override void SetPosition(string position)
        {
            _position = position;

            if (position is "LeftDefender" or "RightDefender")
            {
                forwardRoles.gameObject.SetActive(false);
                defenderRoles.gameObject.SetActive(true);
            }
            else
            {
                forwardRoles.gameObject.SetActive(true);
                defenderRoles.gameObject.SetActive(false);
            }
        }

        public override void MintCard(Dictionary<string, double> royalties)
        {
            dynamic fieldPlayer = new ExpandoObject();
            
            fieldPlayer.type = "FieldPlayer";
            fieldPlayer.position = _position;
            fieldPlayer.role = _role;
            fieldPlayer.hand = hand.isOn ? "Right" : "Left";
            fieldPlayer.number = number.text;
            fieldPlayer.stats = new []
            { 
                int.Parse(iQ.text),
                int.Parse(skating.text),
                int.Parse(shooting.text),
                int.Parse(strength.text),
                int.Parse(morale.text)
            };
            
            string fieldPlayerJson = JsonConvert.SerializeObject(fieldPlayer);
            
            Actions.MintNFT(royalties, ImageUrl, CardName, fieldPlayerJson);
        }
    }
}