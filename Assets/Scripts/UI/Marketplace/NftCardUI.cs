using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public abstract class NftCardUI : MonoBehaviour
    {
        [SerializeField] protected Image image;
        [SerializeField] protected Text price;
        [SerializeField] protected Text ownerId;
        [SerializeField] protected Button chooseButton;
        
        protected ICardRenderer CardRenderer;
        
        private ICardLoader _cardLoader;

        public void PrepareNftCard(ICardLoader cardLoader)
        {
            _cardLoader = cardLoader;
            chooseButton.onClick.AddListener(OnClick);
        }
        
        private void OnClick()
        {
            _cardLoader.LoadCard(CardRenderer);
        }
    }
}