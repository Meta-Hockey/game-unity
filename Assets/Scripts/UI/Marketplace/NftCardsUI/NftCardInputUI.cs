using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardInputUI : MonoBehaviour
    {
        public string CardName { get; set; }
        
        public virtual void SetPosition(string position) { }

        public abstract void MintCard(Dictionary<string, double> royalties, string url);
    }
}