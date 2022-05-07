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
        private string _hand;
        
        [SerializeField] private InputField number;
        
        [SerializeField] private InputField iQ;
        [SerializeField] private InputField skating;
        [SerializeField] private InputField shooting;
        [SerializeField] private InputField strength;
        [SerializeField] private InputField morale;

        public override void SetPosition(string position)
        {
            _position = position;
        }

        public override void MintCard(Dictionary<string, double> royalties, string url)
        {
            dynamic fieldPlayer = new ExpandoObject();
            
            fieldPlayer.type = "FieldPlayer";
            fieldPlayer.position = _position;
            fieldPlayer.role = _role;
            fieldPlayer.hand = _hand;
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
            
            Actions.MintNFT(royalties, url, name, fieldPlayerJson);
        }
    }
}