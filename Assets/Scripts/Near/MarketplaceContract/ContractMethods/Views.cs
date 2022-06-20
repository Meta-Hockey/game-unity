using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Near.Models;
using NearClientUnity;
using NearClientUnity.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Near.MarketplaceContract.ContractMethods
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
                royalty = JsonConvert.DeserializeObject<Dictionary<string, double>>(o.royalty.ToString()),
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

        private static NFT ParseNFT(dynamic dynamicNFT)
        {
            return new NFT()
            {
                token_id = dynamicNFT.token_id,
                owner_id = dynamicNFT.owner_id,
                metadata = new Metadata()
                {
                    title = dynamicNFT.metadata.title,
                    description = dynamicNFT.metadata.description,
                    media = dynamicNFT.metadata.media,
                    media_hash = dynamicNFT.metadata.media_hash,
                    issued_at = dynamicNFT.metadata.issued_at,
                    expires_at = dynamicNFT.metadata.expires_at,
                    starts_at = dynamicNFT.metadata.starts_at,
                    updated_at = dynamicNFT.metadata.updated_at,
                    extra = JsonConvert.DeserializeObject<Extra>(dynamicNFT.metadata.extra.ToString(), new ExtraConverter())
                },
                approved_accounts_ids = dynamicNFT.approved_accounts_ids,
                royalty = dynamicNFT.royalty,
                token_type = dynamicNFT.token_type,
            };
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

        private static async Task<List<NFTSaleInfo>> ConcatNFTSale(List<NFT> nfts, List<Sale> sales, ContractNear marketplaceContract)
        {
            List<NFTSaleInfo> nftSalesInfo = new List<NFTSaleInfo>();

            foreach (NFT nft in nfts)
            {
                string tokenId = nft.token_id;
                Sale sale = sales.Find(s => s.token_id == tokenId);

                if (sale == null)
                {
                    dynamic saleArgs = new ExpandoObject();
                    saleArgs.nft_contract_token = NearPersistentManager.Instance.nftContactId + "||" + tokenId;
                    
                    dynamic dynamicSale = await marketplaceContract.View("get_sale", saleArgs);
                    if (dynamicSale.result.ToString() != "null")
                    {
                        sale = ParseSale(JObject.Parse(dynamicSale.result.ToString()));
                    }
                }
                
                nftSalesInfo.Add(new NFTSaleInfo()
                {
                    NFT = nft,
                    Sale = sale,
                });
            }

            return nftSalesInfo;
        }
        
        public static async Task<List<NFTSaleInfo>> LoadUserNFTs(string fromIndex = "0", int limit = 50)
        {
            List<NFTSaleInfo> nftSalesInfo = new List<NFTSaleInfo>();
            
            string accountId = NearPersistentManager.Instance.GetAccountId();
            if (accountId == "")
            {
                return nftSalesInfo;
            }
            
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            ContractNear marketplaceContract = await NearPersistentManager.Instance.GetMarketplaceContract();
            
            dynamic args = new ExpandoObject();
            
            args.account_id = accountId;
            args.from_index = fromIndex;
            args.limit = limit;
            
            dynamic dynamicNFTs = await nftContract.View("nft_tokens_for_owner", args);
            List<NFT> nfts = DynamicNFTsToList(dynamicNFTs);

            dynamic dynamicSales = await marketplaceContract.View("get_sales_by_owner_id", args);
            List<Sale> sales = DynamicSalesToList(dynamicSales);
            
            return await ConcatNFTSale(nfts, sales, marketplaceContract);
        }

        public static async Task<List<NFTSaleInfo>> LoadSales(string fromIndex = "0", int limit = 50)
        {
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();

            dynamic salesArgs = new ExpandoObject();
            salesArgs.nft_contract_id = NearPersistentManager.Instance.nftContactId;
            salesArgs.from_index = fromIndex;
            salesArgs.limit = limit;
            
            dynamic sales = await marketContract.View("get_sales_by_nft_contract_id", salesArgs);
            List<Sale> salesList = JsonConvert.DeserializeObject<List<Sale>>(sales.result.ToString());
            
            List<string> token_ids = salesList.Where(x => x.nft_contract_id == NearPersistentManager.Instance
                .nftContactId).Select(x => x.token_id).ToList();

            dynamic saleTokensArgs = new ExpandoObject();
            saleTokensArgs.token_ids = token_ids;

            dynamic saleTokensDynamic = await nftContract.View("nft_tokens_batch", saleTokensArgs);
            List<NFT> tokens = DynamicNFTsToList(saleTokensDynamic);

            List<NFTSaleInfo> nftSalesInfo = new List<NFTSaleInfo>();
            
            foreach (Sale sale in salesList)
            {
                NFT token = tokens.Find(t => t.token_id == sale.token_id);

                if (token == null)
                {
                    dynamic tokenArgs = new ExpandoObject();
                    tokenArgs.token_id = sale.token_id;

                    dynamic tokenDynamic = await nftContract.View("nft_token", tokenArgs);
                    token = ParseNFT(JsonConvert.DeserializeObject<dynamic>(tokenDynamic.result.ToString()));
                }
                
                nftSalesInfo.Add(new NFTSaleInfo()
                {
                    NFT = token,
                    Sale = sale,
                });
            }
            
            return nftSalesInfo;
        }

        public static async Task<UInt128> GetPriceForSpot()
        {
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();
            
            dynamic dynamicAmount = await marketContract.View("storage_amount");

            return UInt128.Parse(dynamicAmount.result.ToString());
        }
    }
}