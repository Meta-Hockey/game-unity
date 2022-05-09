using UnityEngine;
using UnityEngine.UI;

namespace UI.Marketplace.Sell_cards
{
    public class SwitchToggleSellCard : MonoBehaviour
    {
        [SerializeField] private Text sellImmediately;
        [SerializeField] private Text sellOnAuction;

        [SerializeField] private Transform sellImmediatelyView;
        [SerializeField] private Transform sellOnAuctionView;
        
        [SerializeField] private Color activeColor;
        [SerializeField] private Color defaultColor;
        
        [SerializeField] private RectTransform uiHandleRectTransform;

        private Toggle _toggle;

        private Vector2 _handlePosition;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _handlePosition = uiHandleRectTransform.anchoredPosition;

            _toggle.onValueChanged.AddListener(OnSwitch);

            if (_toggle.isOn)
            {
                OnSwitch(true);
            }
        }

        private void OnSwitch(bool on)
        {
            if (on)
            {
                uiHandleRectTransform.anchoredPosition = _handlePosition * -1;
                
                sellImmediatelyView.gameObject.SetActive(false);
                sellOnAuctionView.gameObject.SetActive(true);
                
                sellImmediately.color = defaultColor;
                sellOnAuction.color = activeColor;
            }
            else
            {
                uiHandleRectTransform.anchoredPosition = _handlePosition;
                
                sellImmediatelyView.gameObject.SetActive(true);
                sellOnAuctionView.gameObject.SetActive(false);
                
                sellImmediately.color = activeColor;
                sellOnAuction.color = defaultColor;
            }
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnSwitch);
        }
    }
}