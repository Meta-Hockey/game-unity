using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class NftCardDescriptionUI : MonoBehaviour
    {
        [SerializeField] protected Text cardName;

        public Text Name => cardName;
    }
}