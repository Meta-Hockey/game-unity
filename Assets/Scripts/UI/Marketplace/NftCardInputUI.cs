using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public abstract class NftCardInputUI : MonoBehaviour, ICardMinter
    {
        protected InputField Img;

        public virtual void MintCard()
        {
            throw new System.NotImplementedException();
        }
    }
}