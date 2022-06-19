using Near.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.NftCardsUI
{
    public abstract class CardUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text cardName;

        public Image Image => image;
        public Text CardName => cardName;

        public abstract void SetData(NFTSaleInfo nftSaleInfo);
    }
}