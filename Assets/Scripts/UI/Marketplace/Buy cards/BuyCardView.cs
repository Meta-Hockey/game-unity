using Near.Models;
using UI.Marketplace.NftCardsUI;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform cardTileContent;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;

        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo)
        {
            if (_cardTile != null)
            {
                Destroy(_cardTile.gameObject);
            }

            if (_cardDescription != null)
            {
                Destroy(_cardDescription.gameObject);
            }
            
            _cardTile = cardRenderer.RenderCardTile(cardTileContent);
            _cardDescription = cardRenderer.RenderCardDescription(cardDescriptionContent);
            
            viewInteractor.ChangeView(gameObject.transform);
        }
    }
}