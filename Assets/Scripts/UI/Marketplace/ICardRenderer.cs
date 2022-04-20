using UnityEngine;

namespace UI.Marketplace
{
    public interface ICardRenderer
    {
        public Transform RenderCardTile();
        public Transform RenderCardDescription();
    }
}