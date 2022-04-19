using UnityEngine;

namespace UI.Marketplace.MintNft
{
    public class MintNftButton : MonoBehaviour
    {
        [SerializeField] private Transform mintNftScrollView;
        [SerializeField] private ViewInteractor viewInteractor;

        public void MintNft()
        {
            viewInteractor.ChangeView(mintNftScrollView);
        }
    }
}