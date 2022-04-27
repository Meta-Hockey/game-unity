using System;
using System.Collections;
using Near.Models;
using UnityEngine;
using UnityEngine.Networking;
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

        protected NFT CardData;
        
        private ICardLoader _cardLoader;
        
        public Text Name => nameNftCard;
        public Text Price => price;
        public Text OwnerId => ownerId;
        
        protected abstract ICardRenderer GetCardRenderer();
        
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
        
        public void LoadImage(string url)
        {
            StartCoroutine(Utils.Utils.LoadImage(image, url));
        }
    }
}