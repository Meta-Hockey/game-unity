using System.Collections.Generic;
using System.Threading.Tasks;
using Near.MarketplaceContract.ContractMethods;
using Near.Models;

namespace UI.Marketplace
{
    public class MarketplaceController
    {
        private List<NFTSaleInfo> _nftSalesInfo;
        private List<NFTSaleInfo> _userNFTs;

        public async Task<List<NFTSaleInfo>> GetSales()
        {
            _nftSalesInfo ??= await Views.LoadSales();

            return _nftSalesInfo;
        }

        public async Task<List<NFTSaleInfo>> GetUserNFTs()
        {
            _userNFTs ??= await Views.LoadUserNFTs();

            return _userNFTs;
        }
    }
}