using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardDescriptionUI : MonoBehaviour
    {
        [SerializeField] protected Text cardName;
        [SerializeField] protected Text ownerId;
        [SerializeField] protected Text royalty;
        public Text Name => cardName;
        public Text OwnerId => ownerId;
        public Text Royalty => royalty;
    }
}