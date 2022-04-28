using System.Collections.Generic;
using System.Threading.Tasks;
using Near.MarketplaceContract.ContractMethods;
using Near.Models;

namespace UI.Marketplace
{
    public class MarketplaceController
    {
        private List<NFTSaleInfo> _nftSalesInfo;

        public async Task<List<NFTSaleInfo>> GetSales()
        {
            _nftSalesInfo ??= await Views.LoadSales();

            return _nftSalesInfo;
        }
    }
}