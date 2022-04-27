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
        private NftCardInputUI _cardMinter;

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

        public void Mint()
        {
            _cardMinter.MintCard();
        }
    }
}