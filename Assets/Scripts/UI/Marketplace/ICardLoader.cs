using Near.Models;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public interface ICardLoader
    {
        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo);
    }
}