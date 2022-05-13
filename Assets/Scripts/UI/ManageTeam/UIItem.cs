using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;

        [SerializeField] private Transform canvasContent;
        private Transform _currentParent;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _currentParent = _rectTransform.parent;
            _rectTransform.SetParent(canvasContent);
            
            _currentParent.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var scaleFactor = _mainCanvas.scaleFactor;
            _rectTransform.anchoredPosition += eventData.delta / scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Transform itemTransform = eventData.pointerDrag.transform;
            if (itemTransform.parent == canvasContent)
            {
                itemTransform.SetParent(_currentParent);
            }
            
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}