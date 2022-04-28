using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models;

namespace UI.Marketplace
{
    public class MarketplaceController
    {
        private List<NFT> _AllNFTCards;

        public async Task<List<NFT>> GetAllCards()
        {
            _AllNFTCards ??= await Near.MarketplaceContract.Views.LoadCards();

            return _AllNFTCards;
        }
    }
}