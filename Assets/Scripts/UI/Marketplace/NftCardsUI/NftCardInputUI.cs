using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardInputUI : MonoBehaviour
    {
        [SerializeField] protected InputField name;
        
        public abstract void MintCard(Dictionary<string, double> royalties, string url);
    }
}