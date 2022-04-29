using System;
using System.Collections.Generic;
using System.Dynamic;
using NearClientUnity;
using NearClientUnity.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Near.MarketplaceContract.ContractMethods
{
    public static class Actions
    {
        public static async void MintNFT(Dictionary<string, double> royalties, string media, string title, string extra)
        {
            Dictionary<string, int> perpetualRoyalties = new Dictionary<string, int>();

            int totalPerpetual = 0;
            
            foreach (KeyValuePair<string,double> royalty in royalties)
            {
                int amount = (int)(royalty.Value * 100);
                
                totalPerpetual += amount;
                perpetualRoyalties.Add(royalty.Key, amount);
            }

            if (totalPerpetual > NearUtils.MinterRoyaltyCap)
            {
                Debug.Log("Cannot add more than 20% in perpetual NFT royalties when minting");
                return;
            }

            dynamic metadata = new ExpandoObject();
            metadata.title = title;
            metadata.media = media;
            metadata.issued_at = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            metadata.extra = extra;

            dynamic args = new ExpandoObject();
            args.token_id = "token-" + DateTimeOffset.Now.ToUnixTimeMilliseconds();
            args.metadata = metadata;
            args.perpetual_royalties = perpetualRoyalties;
            
            UInt128 deposit = NearUtils.ParseNearAmount("1") / 10;
            
            ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();
            
            await nftContract.Change("nft_mint", args, NearUtils.Gas, deposit);
        }

        public static async void RegisterStorage(UInt128 amount)
        {
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();
            UInt128 price = await Views.GetPriceForSpot();

            await marketContract.Change("storage_deposit", new ExpandoObject(), NearUtils.Gas, price * amount);
        }

        public static async void SaleUpdate(Dictionary<string, UInt128> newSaleConditions, string tokenId, bool isAuction)
        {
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();

            dynamic saleArgs = new ExpandoObject();
            saleArgs.nft_contract_token = NearPersistentManager.Instance.nftContactId + ":" + tokenId;

            dynamic sale = await marketContract.View("get_sale", saleArgs);

            // TODO: Implement for other fts
            if (sale.result.ToString() != "null")
            {
                dynamic updatePriceArgs = new ExpandoObject();
                updatePriceArgs.nft_contract_id = NearPersistentManager.Instance.nftContactId;
                updatePriceArgs.token_id = tokenId;
                updatePriceArgs.ft_token_id = "near";
                updatePriceArgs.price = newSaleConditions["near"];

                await marketContract.Change("update_price", updatePriceArgs, NearUtils.Gas);
            }
            else
            {
                dynamic nftApproveArgs = new ExpandoObject();
                nftApproveArgs.token_id = tokenId;
                nftApproveArgs.account_id = NearPersistentManager.Instance.MarketplaceContactId;

                dynamic msg = new ExpandoObject();
                msg.isAuction = isAuction;
                msg.sale_conditions = JsonConvert.SerializeObject(newSaleConditions);

                nftApproveArgs.msg = JsonConvert.SerializeObject(msg);

                UInt128 deposit = NearUtils.ParseNearAmount("1") / 100;

                ContractNear nftContract = await NearPersistentManager.Instance.GetNftContract();

                await nftContract.Change("nft_approve", nftApproveArgs, NearUtils.Gas, deposit);
            }
        }

        public static async void Offer(string tokenId, string offerToken, string price)
        {
            if (offerToken != "near")
            {
                throw new Exception("currently only accepting NEAR offers");
            }
            
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();

            dynamic offerArgs = new ExpandoObject();
            offerArgs.nft_contract_id = NearPersistentManager.Instance.nftContactId;
            offerArgs.token_id = tokenId;

            await marketContract.Change("offer", offerArgs, NearUtils.Gas, NearUtils.ParseNearAmount(price));
        }

        public static async void AcceptOffer(string tokenId, string ftId)
        {
            if (ftId != "near")
            {
                throw new Exception("currently only accepting NEAR offers");
            }
            
            ContractNear marketContract = await NearPersistentManager.Instance.GetMarketplaceContract();
            
            dynamic acceptOfferArgs = new ExpandoObject();
            acceptOfferArgs.nft_contract_id = NearPersistentManager.Instance.nftContactId;
            acceptOfferArgs.token_id = tokenId;
            acceptOfferArgs.ft_token_id = ftId;

            await marketContract.Change("accept_offer", acceptOfferArgs, NearUtils.Gas);
        }
    }
}