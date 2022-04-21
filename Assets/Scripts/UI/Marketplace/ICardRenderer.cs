using UI.Marketplace.NftCardsUI;
using UnityEngine;

namespace UI.Marketplace
{
    public interface ICardRenderer
    {
        public NftCardUI RenderCardTile(Transform content);
        public NftCardDescriptionUI RenderCardDescription(Transform content);
    }
}