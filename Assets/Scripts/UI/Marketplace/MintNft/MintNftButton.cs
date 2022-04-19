using UnityEngine;

namespace UI.Marketplace.MintNft
{
    public class MintNftButton : MonoBehaviour
    {
        [SerializeField] private Transform buyCardsScrollView;
        [SerializeField] private Transform sellCardsScrollView;
        [SerializeField] private Transform mintNftScrollView;

        public void MintNft()
        {
            mintNftScrollView.gameObject.SetActive(true);
            sellCardsScrollView.gameObject.SetActive(false);
            buyCardsScrollView.gameObject.SetActive(false);

            mintNftScrollView.SetAsLastSibling();
        }
    }
}