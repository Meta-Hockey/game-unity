using System.Collections.Generic;
using DataClasses;
using Runtime;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsController : IController
    {
        public List<NftCard> NftCards { get; private set; }

        public async void Start()
        {
            // NftCards ??= await Near.MarketplaceContract.Views.LoadCards();
        }
    }
}