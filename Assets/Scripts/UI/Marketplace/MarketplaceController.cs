using System.Collections.Generic;
using System.Linq;
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
            
            return _userNFTs.Where(x => x.Sale == null).ToList();
        }

        public async Task<List<NFTSaleInfo>> GetFreeAgents()
        {
            _userNFTs ??= await Views.LoadUserNFTs();
            
            return _userNFTs.Where(x => x.Sale != null).ToList();
        }

        public async Task<string> GetMarketStoragePaid()
        {
            UInt128 amountUInt128 = await Views.GetPriceForSpot();
            
            return NearUtils.FormatNearAmount(amountUInt128).ToString();
        }

        public void RegisterStorage(string amount)
        {
            Actions.RegisterStorage(UInt128.Parse(amount));
        }

        public void SaleUpdate(Dictionary<string, string> newSaleConditions, string tokenId, bool isAuction)
        {
            Actions.SaleUpdate(newSaleConditions, tokenId, isAuction);
        }

        public void RemoveSale(string tokenId)
        {
            Actions.RemoveSale(tokenId);
        } 

        public void Offer(string tokenId, string offerToken, string price)
        {
            Actions.Offer(tokenId, offerToken, price);
        }

        public void AcceptOffer(string tokenId)
        {
            Actions.AcceptOffer(tokenId);
        }
    }
}