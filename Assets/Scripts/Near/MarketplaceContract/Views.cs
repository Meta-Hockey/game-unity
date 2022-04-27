using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Near.Models;
using NearClientUnity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Near.MarketplaceContract
{
    public static class Views
    {
        public static async Task<List<NFT>> LoadCards(string fromIndex = "0", int limit = 50)
        {
            ContractNear gameContract = await NearPersistentManager.Instance.GetNftContract();
            dynamic args = new ExpandoObject();
            args.from_index = fromIndex;
            args.limit = 50;

            dynamic cards = await gameContract.View("nft_tokens", args);
            IEnumerable<dynamic> listDynamicNft = JsonConvert
                .DeserializeObject<List<dynamic>>(cards.result.ToString());

            IEnumerable<NFT> nfts = listDynamicNft.Select(o => new NFT()
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
            // const saleTokens = await nftContract.nft_tokens_batch({
            //     token_ids: sales.filter(({ nft_contract_id }) => nft_contract_id === nftContractName).map(({ token_id }) => token_id)
            // });
            // dynamic tokenIds = sales.
            // dynamic saleTokens = await nftContract.View("nft_tokens_batch", );
            return sales;
        }
    }
}