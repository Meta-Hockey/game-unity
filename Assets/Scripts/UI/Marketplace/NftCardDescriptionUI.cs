using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public abstract class NftCardDescriptionUI : MonoBehaviour
    {
        [SerializeField] protected Image img;
        [SerializeField] protected Text price;
        [SerializeField] protected Text ownerId;

        public Image Image => img;
        public Text Price => price;
        public Text OwnerId => ownerId;
    }
}