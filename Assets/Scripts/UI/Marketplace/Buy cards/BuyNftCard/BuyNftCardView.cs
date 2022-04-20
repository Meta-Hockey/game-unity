using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Buy_cards.BuyNftCard
{
    public class BuyNftCardView : MonoBehaviour, ICardLoader
    {
        [SerializeField] private Image image;
        [SerializeField] private Text price;
        [SerializeField] private Text ownerId;

        [SerializeField] private Text playerName;
        [SerializeField] private Text playerType;
        [SerializeField] private Text playerRole;
        [SerializeField] private Text playerPosition;
        [SerializeField] private Text playerStats;
        
        [SerializeField] private ViewInteractor viewInteractor;

        public void LoadCard(ICardRenderer cardRenderer)
        {
            // TODO get prefabs from cardRenderer and insert them in view
            price.text = "Price: 5N";
            ownerId.text = "Owner id: Yurii";
            
            viewInteractor.ChangeView(gameObject.transform);
        }
    }
}