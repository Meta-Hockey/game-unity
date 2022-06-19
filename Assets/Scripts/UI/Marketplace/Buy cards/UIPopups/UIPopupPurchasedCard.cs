using System;
using Near;
using Near.Models;
using NearClientUnity.Utilities;
using Runtime;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Buy_cards.UIPopups
{
    public class UIPopupPurchasedCard : UIPopup
    {
        [SerializeField] private Text price;
        
        public void SetData(NFTSaleInfo nftSaleInfo, Transform content)
        {
            CardUI card = nftSaleInfo.NFT.metadata.extra.Type switch
            {
                "FieldPlayer" => Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardUI, content),
                "Goalie" => Instantiate(Game.AssetRoot.marketplaceAsset.goalieCardUI, content),
                "GoaliePos" => Instantiate(Game.AssetRoot.marketplaceAsset.goalieCardUI, content),
                _ => throw new Exception("Extra type not found")
            };
            
            card.SetData(nftSaleInfo);
            price.text = NearUtils.FormatNearAmount(UInt128.Parse(nftSaleInfo.Sale.sale_conditions["near"])).ToString();
        }
    }
}