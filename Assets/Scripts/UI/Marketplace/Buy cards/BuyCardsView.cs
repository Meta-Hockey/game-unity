using System;
using System.Collections.Generic;
using Near.Models;
using Newtonsoft.Json.Linq;
using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private BuyCardView buyNftCardView;

        private BuyCardsController _buyCardsController;

        private bool _isLoaded;

        private void Awake()
        {
            _buyCardsController = new BuyCardsController();
            _isLoaded = false;
        }

        public async void LoadNftCards()
        {
            viewInteractor.ChangeView(gameObject.transform);
            
            if (_isLoaded)
            {
                return;
            }

            List<NFT> nftCards = await _buyCardsController.GetAllCards();

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