using System;
using System.Collections.Generic;
using System.Dynamic;
using NearClientUnity;
using NearClientUnity.Utilities;
using UnityEngine;

namespace Near.MarketplaceContract
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
            
            await nftContract.Change("nft_mint", args, NearUtils.GasMint, deposit);
        }
    }
}