using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI.FieldPlayer
{
    public class FieldPlayerInputUI : NftCardInputUI
    {
        [SerializeField] private Dropdown position;
        [SerializeField] private Dropdown role;
        [SerializeField] private Dropdown hand;
        
        [SerializeField] private InputField number;
        
        [SerializeField] private InputField iQ;
        [SerializeField] private InputField skating;
        [SerializeField] private InputField shooting;
        [SerializeField] private InputField strength;
        [SerializeField] private InputField morale;

        public override void MintCard(Dictionary<string, double> royalties, string url)
        {
            dynamic fieldPlayer = new ExpandoObject();
            
            fieldPlayer.type = "FieldPlayer";
            fieldPlayer.position = position.options[position.value].text;
            fieldPlayer.role = role.options[role.value].text;
            fieldPlayer.hand = hand.options[hand.value].text;
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
            
            Near.MarketplaceContract.Actions.MintNFT(royalties, url, name.text, fieldPlayerJson);
        }
    }
}