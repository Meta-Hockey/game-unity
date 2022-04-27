using UnityEngine;

namespace UI.Marketplace
{
    public abstract class NftCardInputUI : MonoBehaviour
    {
        protected string Image;

        public abstract void MintCard();
    }
}