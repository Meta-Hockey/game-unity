using UnityEngine;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Transform cardTileContent;
        [SerializeField] private Transform cardDescriptionContent;
        [SerializeField] private ViewInteractor viewInteractor;

        private NftCardUI _cardTile;
        private NftCardDescriptionUI _cardDescription;

        public void LoadCard(ICardRenderer cardRenderer)
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