using Newtonsoft.Json.Linq;
using Runtime;
using UnityEngine;

namespace UI.Marketplace.Sell_cards
{
    public class SellCardsView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private ViewInteractor viewInteractor;
        [SerializeField] private SellCardView sellNftCardView;
        
        private bool _isLoaded;

        private void Awake()
        {
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
                data["name"] = "Yurii";
                data["ownerId"] = "Owner id: kastet99";
                data["price"] = i + "N";

                data["type"] = "Type: Field player";
                data["position"] = "Position: Center";
                data["role"] = "Role: Shooter";

                data["skating"] = "Skating: " + i * 5.3;
                data["shooting"] = "Shooting: " + i * 4.6;
                data["strength"] = "Strength: " + i * 6.4;
                data["iq"] = "IQ: " + i * 2.5;
                data["morale"] = "Morale: " + i * 5.9;

                card.PrepareNftCard(sellNftCardView, data, content);
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

                data["gloveAndBlocker"] = "GloveAndBlocker: " + i * 5.3;
                data["pads"] = "Pads: " + i * 4.5;
                data["stand"] = "Stand: " + i * 6.2;
                data["stretch"] = "stretch: " + i * 2.1;
                data["morale"] = "Morale: " + i * 5.7;

                card.PrepareNftCard(sellNftCardView, data, content);
            }

            _isLoaded = true;
        }
    }
}