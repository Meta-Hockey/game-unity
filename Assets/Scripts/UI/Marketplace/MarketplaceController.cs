using System.Collections.Generic;
using System.Threading.Tasks;
using Near;
using Near.MarketplaceContract.ContractMethods;
using Near.Models;
using NearClientUnity.Utilities;

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

        public async Task<double> GetMarketStoragePaid()
        {
            UInt128 amountUInt128 = await Views.GetPriceForSpot();
            
            return NearUtils.FormatNearAmount(amountUInt128);
        }

        public void RegisterStorage(string amount)
        {
            Actions.RegisterStorage(UInt128.Parse(amount));
        }

        public void SaleUpdate(Dictionary<string, UInt128> newSaleConditions, string tokenId, bool isAuction)
        {
            Actions.SaleUpdate(newSaleConditions, tokenId, isAuction);
        }
    }
}