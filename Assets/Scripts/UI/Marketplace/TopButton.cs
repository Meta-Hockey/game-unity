using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public class TopButton : MonoBehaviour
    {
        [SerializeField] private MarketplaceInteractor marketplaceInteractor;
            
        public Text text;
        public Image image;
        
        public Sprite activeSprite;
        public Sprite defaultSprite;
        public Color activeColor;
        
        public void ChangeActiveButton()
        {
            marketplaceInteractor.ChangeActiveButton(this);
        }
    }
}