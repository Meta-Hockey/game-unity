using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Near.Models;
using NearClientUnity;
using Newtonsoft.Json;

namespace Near.MarketplaceContract
{
    public static class Views
    {
        private static List<NFT> DynamicNFTsToList(dynamic dynamicNFTs)
        {
            IEnumerable<dynamic> listDynamicNFT = JsonConvert
                .DeserializeObject<List<dynamic>>(dynamicNFTs.result.ToString());
            
            IEnumerable<NFT> nfts = listDynamicNFT.Select(o => new NFT()
            {
                token_id = o.token_id,
                owner_id = o.owner_id,
                metadata = new Metadata()
                {
                    title = o.metadata.title,
                    description = o.metadata.description,
                    media = o.metadata.media,
                    media_hash = o.metadata.media_hash,
                    issued_at = o.metadata.issued_at,
                    expires_at = o.metadata.expires_at,
                    starts_at = o.metadata.starts_at,
                    updated_at = o.metadata.updated_at,
                    extra = JsonConvert.DeserializeObject<Extra>(o.metadata.extra.ToString(), new ExtraConverter())
                },
                approved_accounts_ids = o.approved_accounts_ids,
                royalty = o.royalty,
                token_type = o.token_type,
            });

            return nfts.ToList();
        }

        private static List<Sale> DynamicSalesToList(dynamic dynamicSales)
        {
            IEnumerable<dynamic> listDynamicSales = JsonConvert
                .DeserializeObject<List<dynamic>>(dynamicSales.result.ToString());

            IEnumerable<Sale> sales = listDynamicSales.Select(o => new Sale()
            {
                owner_id = o.owner_id,
                nft_contract_id = o.nft_contract_id,
                token_id = o.token_id,
                token_type = o.token_type,
                approval_id = ulong.Parse(o.approval_id.ToString()),
                created_at = ulong.Parse(o.created_at.ToString()),
                is_auction = bool.Parse(o.is_auction.ToString()),
                sale_conditions = JsonConvert.DeserializeObject<Dictionary<string, String>>(o.sale_conditions.ToString()),
                bids = JsonConvert.DeserializeObject<Dictionary<string, List<Bid>>>(o.bids.ToString())
            });

            return sales.ToList();
        }

        private static Sale ParseSale(dynamic sale)
        {
            return new Sale()
            {
                owner_id = sale.owner_id,
                nft_contract_id = sale.nft_contract_id,
                token_id = sale.token_id,
                token_type = sale.token_type,
                approval_id = ulong.Parse(sale.approval_id.ToString()),
                created_at = ulong.Parse(sale.created_at.ToString()),
                is_auction = bool.Parse(sale.is_auction.ToString()),
                sale_conditions = JsonConvert.DeserializeObject<Dictionary<string, String>>(sale.sale_conditions.ToString()),
                bids = JsonConvert.DeserializeObject<Dictionary<string, List<Bid>>>(sale.bids.ToString())
            };
        }

        public static async Task<List<NFT>> LoadCards(string fromIndex = "0", int limit = 50)
        {
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            dynamic args = new ExpandoObject();
            args.from_index = fromIndex;
            args.limit = 50;

            dynamic dynamicNFTs = await nftContract.View("nft_tokens", args);

            return DynamicNFTsToList(dynamicNFTs);
        }

        public static async Task<dynamic> LoadUserNFTs(string fromIndex = "0", int limit = 50)
        {
            List<NFT> nfts = new List<NFT>();

            string accountId = NearPersistentManager.Instance.GetAccountId();
            if (accountId == "")
            {
                return nfts;
            }
            
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            ContractNear marketplaceContract = await NearPersistentManager.Instance.GetMarketplaceContract();
            
            dynamic args = new ExpandoObject();
            
            args.account_id = accountId;
            args.from_index = fromIndex;
            args.limit = limit;

            dynamic dynamicNFTs = await nftContract.View("nft_tokens_for_owner", args);
            dynamic dynamicSales = await marketplaceContract.View("get_sales_by_owner_id", args);

            nfts = DynamicNFTsToList(dynamicNFTs);
            List<Sale> sales = DynamicSalesToList(dynamicSales);

            foreach (NFT nft in nfts)
            {
                string tokenId = nft.token_id;
                Sale sale = sales.Find(x => x.token_id == tokenId);

                if (sale == null)
                {
                    dynamic saleArgs = new ExpandoObject();
                    saleArgs.nft_contract_token = NearPersistentManager.Instance.nftContactId + "||" + tokenId;
                    
                    dynamic dynamicSale = await marketplaceContract.View("get_sale", saleArgs);
                    sale = ParseSale(dynamicSale);
                }

                nft.Sale = sale;
            }

            return nfts;
        }

        public static async Task<dynamic> LoadSales(string fromIndex = "0", int limit = 50)
        {
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();

            dynamic salesArgs = new ExpandoObject();
            salesArgs.nft_contract_id = NearPersistentManager.Instance.nftContactId;
            salesArgs.from_index = fromIndex;
            salesArgs.limit = limit;
            
            dynamic sales = await marketContract.View("get_sales_by_nft_contract_id", salesArgs);
            List<Sale> salesList = JsonConvert.DeserializeObject<List<Sale>>(sales.resulg.ToString());
            List<string> token_ids = salesList.Where(x => x.nft_contract_id == NearPersistentManager.Instance
                .nftContactId).Select(x => x.token_id).ToList();
            // const saleTokens = await nftContract.nft_tokens_batch({
            //     token_ids: sales.filter(({ nft_contract_id }) => nft_contract_id === nftContractName).map(({ token_id }) => token_id)
            // });
            // dynamic tokenIds = sales.
            // dynamic saleTokens = await nftContract.View("nft_tokens_batch", );
            return sales;
        }
    }
}