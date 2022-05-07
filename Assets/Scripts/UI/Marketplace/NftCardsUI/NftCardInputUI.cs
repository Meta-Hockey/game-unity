using System.Collections.Generic;
using UnityEngine;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardInputUI : MonoBehaviour
    {
        public string CardName { get; set; }
        public string ImageUrl { get; set; }
        
        public virtual void SetPosition(string position) { }
        
        public virtual void SetRole(string role, RolesButton activeButton) {}

        public abstract void MintCard(Dictionary<string, double> royalties);
    }
}