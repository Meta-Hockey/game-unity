using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        private Vector3 _localPosition;
        
        [SerializeField] private Transform content;
        
        private Transform _parent;

        private Transform _parentContent;

        private Transform _parentSlot;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Transform slotParent = _rectTransform.parent.parent.parent;
            _parent = eventData.pointerDrag.transform.parent;
            
            if (_parent == content)
            {
                _parentContent = slotParent;
            }
            else
            {
                _parentSlot = slotParent;
            }
            
            eventData.pointerDrag.transform.SetParent(slotParent);
            slotParent.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
            _localPosition = transform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var scaleFactor = _mainCanvas.scaleFactor;
            _rectTransform.anchoredPosition += eventData.delta / scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Transform otherItemTransform = eventData.pointerDrag.transform;
            
            if (otherItemTransform.parent == _parentContent)
            {
                transform.localPosition = _localPosition;
                eventData.pointerDrag.transform.SetParent(content);
            }
            else if (otherItemTransform.parent == _parentSlot)
            {
                eventData.pointerDrag.transform.SetParent(_parent);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
            
            _canvasGroup.blocksRaycasts = true;

            _parentSlot = null;
            _parentContent = null;
        }
    }
}