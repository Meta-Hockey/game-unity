using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public abstract class NftCardUI : MonoBehaviour
    {
        [SerializeField] protected Image image;
        [SerializeField] protected Text nameNftCard;
        [SerializeField] protected Text price;
        [SerializeField] protected Text ownerId;
        [SerializeField] protected Button chooseButton;

        // TODO: abstract class for card data
        protected dynamic CardData;
        
        private ICardLoader _cardLoader;

        public Image Image => image;
        public Text Name => nameNftCard;
        public Text Price => price;
        public Text OwnerId => ownerId;
        

        protected virtual ICardRenderer GetCardRenderer()
        {
            throw new Exception("method not implemented");
        }
        
        public void PrepareNftCard(ICardLoader cardLoader, dynamic cardData, Transform content)
        {
            _cardLoader = cardLoader;
            
            CardData = cardData;

            ICardRenderer cardRenderer = GetCardRenderer();
            var cardTile = cardRenderer.RenderCardTile(content);
            
            cardTile.chooseButton.onClick.AddListener(OnClick);
        }
        
        private void OnClick()
        {
            _cardLoader.LoadCard(GetCardRenderer());
        }
    }
}