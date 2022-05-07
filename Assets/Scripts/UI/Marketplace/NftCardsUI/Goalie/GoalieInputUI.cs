using System.Collections.Generic;
using System.Dynamic;
using Near.MarketplaceContract.ContractMethods;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.Goalie
{
    public class GoalieInputUI : NftCardInputUI
    {
        private string _role;

        [SerializeField] private List<RolesButton> rolesButtons;
        
        [SerializeField] private Toggle hand;
        [SerializeField] private InputField number;
        
        [SerializeField] private InputField gloveAndBlocker;
        [SerializeField] private InputField pads;
        [SerializeField] private InputField stand;
        [SerializeField] private InputField stretch;
        [SerializeField] private InputField morale;

        public override void SetRole(string role, RolesButton activeButton)
        {
            foreach (RolesButton button in rolesButtons)
            {
                button.text.color = Color.black;
                button.image.sprite = button.defaultSprite;
            }

            activeButton.text.color = Color.white;
            activeButton.image.sprite = activeButton.activeSprite;

            _role = role;
        }
        
        public override void MintCard(Dictionary<string, double> royalties)
        {
            dynamic goalie = new ExpandoObject();

            goalie.type = "Goalie";
            goalie.position = "GoaliePos";
            goalie.role = _role;
            goalie.hand = hand.isOn ? "Right" : "Left";
            goalie.number = number.text;
            goalie.stats = new []
            { 
                int.Parse(gloveAndBlocker.text),
                int.Parse(pads.text),
                int.Parse(stand.text),
                int.Parse(stretch.text),
                int.Parse(morale.text)
            };
            
            string goalieJson = JsonConvert.SerializeObject(goalie);
            
            Actions.MintNFT(royalties, ImageUrl, CardName, goalieJson);
        }
    }
}