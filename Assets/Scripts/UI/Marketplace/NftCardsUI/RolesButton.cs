using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public class RolesButton : MonoBehaviour
    {
        [SerializeField] private NftCardInputUI view;
        
        public Image image;
        public Sprite activeSprite;
        public Sprite defaultSprite;
        public Text text;
        
        public void SetRole(string role)
        {
            view.SetRole(role, this);
        }
    }
}