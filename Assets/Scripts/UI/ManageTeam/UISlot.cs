using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        private Transform _item;
        
        public void OnDrop(PointerEventData eventData)
        {
            Transform otherItemTransform = eventData.pointerDrag.transform;

            if (_item != null)
            {
                _item.SetParent(otherItemTransform.parent);
            }

            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
            
            _item = otherItemTransform;
        }
    }
}