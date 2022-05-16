using Near.Models;

namespace UI.Marketplace
{
    public interface ICardLoader
    {
        public void LoadCard(ICardRenderer cardRenderer, NFTSaleInfo nftSaleInfo);
    }
}