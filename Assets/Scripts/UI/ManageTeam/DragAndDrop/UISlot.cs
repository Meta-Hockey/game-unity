using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam.DragAndDrop
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public UIPlayer uiPlayer;
        
        public void OnDrop(PointerEventData eventData)
        {
            UIPlayer uiPlayerDropped = eventData.pointerDrag.GetComponent<UIPlayer>();
            
            if (uiPlayerDropped == null || uiPlayer == null)
            {
                return;
            }
            
            Transform uiPlayerTransform = uiPlayer.transform;
            uiPlayerTransform.SetParent(uiPlayerDropped.currentParent);
            uiPlayerTransform.localPosition = Vector3.zero;

            Transform uiPlayerDroppedTransform = uiPlayerDropped.transform;

            uiPlayerDroppedTransform.SetParent(transform); 
            uiPlayerDroppedTransform.localPosition = Vector3.zero;
            
            UISlot uiDroppedSlot = uiPlayerDropped.uiSlot;

            uiPlayerDropped.uiSlot.uiPlayer = uiPlayer;
            uiPlayerDropped.uiSlot = this;
            
            uiPlayer = uiPlayerDropped;
            
            uiPlayer.uiSlot = uiDroppedSlot;
        }
    }
}