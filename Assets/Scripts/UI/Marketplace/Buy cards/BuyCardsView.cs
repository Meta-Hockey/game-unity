using Newtonsoft.Json.Linq;
using Runtime;
using UnityEngine;

namespace UI.Marketplace.Buy_cards
{
    public class BuyCardsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private BuyNftCardView buyNftCardView;

        private BuyCardsController _buyCardsController;

        private bool _isLoaded;

        private void Awake()
        {
            _buyCardsController = new BuyCardsController();
            _isLoaded = false;
        }

        public void LoadNftCards()
        {
            viewInteractor.ChangeView(gameObject.transform);
            
            if (_isLoaded)
            {
                return;
            }
            
            // TODO: _buyCardsController.Start();
            
            for (int i = 0; i < 4; i++)
            {
                NftCardUI card = Game.AssetRoot.marketplaceAsset.fieldPlayerCardTile;

                // TODO: transfer data to card
                dynamic data = new JObject();
                data["name"] = "Alex";
                data["ownerId"] = "Owner id: yurii";
                data["price"] = i + "N";

                data["type"] = "Type: Field player";
                data["position"] = "Position: Center";
                data["role"] = "Role: Shooter";

                data["skating"] = "Skating: " + i * 5;
                data["shooting"] = "Shooting: " + i * 4;
                data["strength"] = "Strength: " + i * 6;
                data["iq"] = "IQ: " + i * 2;
                data["morale"] = "Morale: " + i * 5;

                card.PrepareNftCard(buyNftCardView, data, content);
            }
            
            for (int i = 0; i < 4; i++)
            {
                NftCardUI card = Game.AssetRoot.marketplaceAsset.goalieNftCardUI;
                
                // TODO: transfer data to card
                dynamic data = new JObject();
                data["name"] = "Alex";
                data["ownerId"] = "Owner id: yurii";
                data["price"] = i + "N";

                data["type"] = "Type: Goalie";
                data["position"] = "Goalie";
                data["role"] = "Wall";

                data["gloveAndBlocker"] = "GloveAndBlocker: " + i * 5;
                data["pads"] = "Pads: " + i * 4;
                data["stand"] = "Stand: " + i * 6;
                data["stretch"] = "stretch: " + i * 2;
                data["morale"] = "Morale: " + i * 5;

                card.PrepareNftCard(buyNftCardView, data, content);
            }

            _isLoaded = true;
        }
    }
}