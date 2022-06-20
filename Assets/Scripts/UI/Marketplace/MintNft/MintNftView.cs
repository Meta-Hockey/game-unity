using System.Collections.Generic;
using Runtime;
using UI.Marketplace.NftCardsUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Marketplace.MintNft
{
    public class MintNftView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        
        [SerializeField] private Image image;

        [SerializeField] private InputField cardName;
        [SerializeField] private InputField imageURL;

        [SerializeField] private Transform playerType;
        [SerializeField] private List<PositionButton> positionButtons;
        
        private NftCardInputUI _cardMinter;
        private Dictionary<string, double> _royalties;

        private void Start()
        {
            _royalties = new Dictionary<string, double> {{Near.NearPersistentManager.Instance.MarketplaceContactId, 15}};
        }

        public void SwitchType(int id)
        {
            switch (id)
            {
                case 0:
                    playerType.gameObject.SetActive(false);
                    break;
                case 1:
                    playerType.gameObject.SetActive(true);
                    break;
            }
        }

        public void ChoosePlayerPosition(string position, PositionButton buttonToChange)
        {
            foreach (PositionButton button in positionButtons)
            {
                button.text.color = Color.black;
                button.image.color = Color.white;
            }

            buttonToChange.image.color = Color.blue;
            buttonToChange.text.color = Color.white;

            NftCardInputUI card;
            if (position == "GoaliePos")
            {
                card = Instantiate(Game.AssetRoot.marketplaceAsset.goalieInputUI, content);
            }
            else
            {
                card = Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerInputUI, content);
                card.SetPosition(position);
            }
            
            ChangePlayerType(card);

        }

        private void ChangePlayerType(NftCardInputUI nftCardInputUI)
        {
            if (_cardMinter != null)
            {
                Destroy(_cardMinter.gameObject);
            }

            _cardMinter = nftCardInputUI;
        }

        public void LoadImage()
        {
            StartCoroutine(Utils.Utils.LoadImage(image, imageURL.text));
        }

        public void Mint()
        {
            _cardMinter.CardName = cardName.text;
            _cardMinter.ImageUrl = imageURL.text;
            
            _cardMinter.MintCard(_royalties);
        }

        public void Back()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}