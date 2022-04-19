using System;
using Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace
{
    public class NftCardInfoUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text price;
        [SerializeField] private Text ownerId;

        [SerializeField] private Text playerName;
        [SerializeField] private Text playerType;
        [SerializeField] private Text playerRole;
        [SerializeField] private Text playerPosition;
        [SerializeField] private Text playerStats;

        [SerializeField] private Button chooseButton;
        
        public void SetNftCard()
        {
            chooseButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
        }
    }
}