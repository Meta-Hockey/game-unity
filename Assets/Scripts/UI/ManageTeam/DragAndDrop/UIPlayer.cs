using Near.Models;
using Near.Models.Team.Team;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ManageTeam.DragAndDrop
{
    public abstract class UIPlayer : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] protected Text playerName;
        [SerializeField] protected Text position;
        [SerializeField] protected Text number;
        [SerializeField] protected Text role;
        [SerializeField] protected Image silverStroke;

        public Image playerImg;
        
        private CanvasGroup _canvasGroup;
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;

        public Transform canvasContent;
        public Transform currentParent;

        public NFTMetadata CardData;
        
        protected void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            currentParent = _rectTransform.parent;
            _rectTransform.SetParent(canvasContent);
            
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
                itemTransform.SetParent(currentParent);
                itemTransform.localPosition = Vector3.zero;
            }
            
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }

        public abstract void SetData(NFTMetadata nftMetadata);
    }
}