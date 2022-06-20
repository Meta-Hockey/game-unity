using System.Collections.Generic;
using System.Dynamic;
using Near.MarketplaceContract.ContractMethods;
using Newtonsoft.Json;
using Runtime;
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
                button.text.color = Color.white;
                button.image.sprite = button.defaultSprite;
            }

            activeButton.text.color = Color.black;
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
            goalie.number = int.Parse(number.text);
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

        public override void ShowMintedCard(Transform content)
        {
            GoalieCardUI goalieCardUI = Instantiate(Game.AssetRoot.marketplaceAsset.goalieCardUI, content);

            StartCoroutine(Utils.Utils.LoadImage(goalieCardUI.Image, ImageUrl));
            
            goalieCardUI.CardName.text = CardName;
            goalieCardUI.Number.text = number.text;
            goalieCardUI.Position.text = "GoaliePos";
            goalieCardUI.Role.text = _role;

            goalieCardUI.Pads.text = pads.text;
            goalieCardUI.GloveAndBlocker.text = gloveAndBlocker.text;
            goalieCardUI.Stretch.text = stretch.text;
            goalieCardUI.Stand.text = stand.text;
            goalieCardUI.Morale.text = morale.text;
        }
    }
}