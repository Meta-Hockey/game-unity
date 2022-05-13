using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Transform otherItemTransform = eventData.pointerDrag.transform;

            otherItemTransform.SetParent(transform);
            otherItemTransform.localPosition = Vector3.zero;
        }
    }
}