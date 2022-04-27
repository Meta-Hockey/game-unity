using System.Collections.Generic;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.MintNft
{
    public class MintNftView : MonoBehaviour
    {
        [SerializeField] private Transform content;
        
        [SerializeField] private Image image;
        [SerializeField] private InputField imageURL;
        
        [SerializeField] private InputField royaltyInputField;
        [SerializeField] private InputField royaltyReceiverInputField;

        [SerializeField] private Text royalties;

        private NftCardInputUI _cardMinter;
        private Dictionary<string, double> _royalties;

        private void Awake()
        {
            _royalties = new Dictionary<string, double>();
        }

        public void SwitchType(int id)
        {
            switch (id)
            {
                case 0:
                    ChangeType(null);
                    break;
                case 1:
                    NftCardInputUI fieldPlayerInput =
                        Instantiate(Game.AssetRoot.marketplaceAsset.fieldPlayerInputUI, content);
                    
                    ChangeType(fieldPlayerInput);
                    break;
                case 2:
                    NftCardInputUI goalieInput =
                        Instantiate(Game.AssetRoot.marketplaceAsset.goalieInputUI, content);
                    
                    ChangeType(goalieInput);
                    break;
            }
        }

        private void ChangeType(NftCardInputUI nftCardInputUI)
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

        public void AddRoyalty()
        {
            // TODO: check account availability
            _royalties.Add(royaltyReceiverInputField.text, double.Parse(royaltyInputField.text));
            ShowRoyalties();
        }

        private void ShowRoyalties()
        {
            royalties.text = "";
            
            foreach (KeyValuePair<string, double> royalty in _royalties)
            {
                royalties.text += royalty.Key + " - " + royalty.Value + "%";
            }
        }

        public void Mint()
        {
            _cardMinter.MintCard(_royalties, imageURL.text);
        }
    }
}