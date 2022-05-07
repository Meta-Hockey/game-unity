using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] private Text leftHand;
        [SerializeField] private Text rightHand;
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
                leftHand.transform.localScale = new Vector3(1, 1, 1);
                rightHand.transform.localScale = new Vector3(2, 2, 1);

                leftHand.color = defaultColor;
                rightHand.color = activeColor;
            }
            else
            {
                uiHandleRectTransform.anchoredPosition = _handlePosition;
                leftHand.transform.localScale = new Vector3(2, 2, 1);
                rightHand.transform.localScale = new Vector3(1, 1, 1);

                leftHand.color = activeColor;
                rightHand.color = defaultColor;
            }
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnSwitch);
        }
    }
}