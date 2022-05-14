using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        private UIItem _uiItem;
        
        public void OnDrop(PointerEventData eventData)
        {
            UIItem uiItem = eventData.pointerDrag.GetComponent<UIItem>();
            
            if (uiItem == null)
            {
                return;
            }

            if (_uiItem != null)
            {
                _uiItem.transform.SetParent(uiItem.currentParent);
            }
            
            Transform uiItemTransform = uiItem.transform;
            uiItemTransform.SetParent(transform); 
            uiItemTransform.localPosition = Vector3.zero;

            _uiItem = uiItem;
        }
    }
}