using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardDescriptionUI : MonoBehaviour
    {
        [SerializeField] protected Text cardName;
        [SerializeField] protected Text ownerId;
        public Text Name => cardName;
        public Text OwnerId => ownerId;
    }
}