using System;
using System.Collections.Generic;
using Near.Models;
using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private BuyCardView buyNftCardView;
        
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

            List<NFT> nftCards = await viewInteractor.MarketplaceController.GetAllCards();

            foreach (NFT nftCard in nftCards)
            {
                NftCardUI card = nftCard.metadata.extra.Type switch
                {
                    "FieldPlayer" => Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile,
                    "Goalie" => Game.AssetRoot.marketplaceAsset.goalieNftCardUI,
                    _ => throw new Exception("Extra type not found")
                };

                card.PrepareNftCard(buyNftCardView, nftCard, content);
            }

            _isLoaded = true;
        }
    }
}