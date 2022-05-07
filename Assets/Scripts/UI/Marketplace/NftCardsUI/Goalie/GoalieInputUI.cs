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
        private string _hand;
        
        [SerializeField] private InputField number;
        
        [SerializeField] private InputField gloveAndBlocker;
        [SerializeField] private InputField pads;
        [SerializeField] private InputField stand;
        [SerializeField] private InputField stretch;
        [SerializeField] private InputField morale;

        public override void MintCard(Dictionary<string, double> royalties, string url)
        {
            dynamic goalie = new ExpandoObject();
            
            goalie.type = "GoaliePos";
            goalie.position = "GoaliePos";
            goalie.role = _role;
            goalie.hand = _hand;
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
            
            Actions.MintNFT(royalties, url, name, goalieJson);
        }
    }
}