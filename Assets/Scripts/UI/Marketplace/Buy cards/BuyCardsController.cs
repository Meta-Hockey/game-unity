using System.Collections.Generic;
using System.Threading.Tasks;
using Near.Models;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsController
    {
        private List<NFT> _nftCards;

        public async Task<List<NFT>> GetAllCards()
        {
            _nftCards ??= await Near.MarketplaceContract.Views.LoadCards();

            return _nftCards;
        }
    }
}