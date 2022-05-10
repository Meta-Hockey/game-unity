using System;
using System.Collections.Generic;
using Near.Models;
using Runtime;
using UI.Marketplace.NftCardsUI;
using UnityEngine;

namespace UI.Marketplace.FreeAgents
{
    public class FreeAgentsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private FreeAgentView freeAgentView;
        
        private bool _isLoaded;

        private void Awake()
        {
            _isLoaded = false;
        }

        public async void LoadNftCards()
        {
            viewInteractor.ChangeView(gameObject.transform);
            
            if (_isLoaded)
            {
                return;
            }
            
            List<NFTSaleInfo> nftSalesInfo = await viewInteractor.MarketplaceController.GetFreeAgents();

            foreach (NFTSaleInfo nftSaleInfo in nftSalesInfo)
            {
                NftCardUI card = nftSaleInfo.NFT.metadata.extra.Type switch
                {
                    "FieldPlayer" => Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile),
                    "Goalie" => Instantiate(Game.AssetRoot.marketplaceAsset.goalieNftCardUI),
                    "GoaliePos" => Instantiate(Game.AssetRoot.marketplaceAsset.goalieNftCardUI),
                    _ => throw new Exception("Extra type not found")
                };

                card.PrepareNftCard(freeAgentView, nftSaleInfo, content);
            }

            _isLoaded = true;
        }
    }
}