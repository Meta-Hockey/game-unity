using UnityEngine;

namespace UI.Marketplace
{
    public class MarketplaceUI
    {
        [SerializeField] private NftCardInfoUI _nftCardInfoUIPrefab;

        private void ConstructNftCardsList()
        {
            _nftCardInfoUIPrefab.SetNftCard();
        }

        private void OnSellCardClick()
        {
        }

        private void OnBuyCardClick()
        {
        }
    }
}