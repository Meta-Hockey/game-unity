using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.ManageTeam.DragAndDrop
{
    public class UISlot : MonoBehaviour, IDropHandler
    {
        public UIPlayer uiPlayer;
        
        public void OnDrop(PointerEventData eventData)
        {
            UIPlayer uiPlayer = eventData.pointerDrag.GetComponent<UIPlayer>();
            
            if (uiPlayer == null)
            {
                return;
            }
            
            Transform uiItemTransform = uiPlayer.transform;

            if (uiPlayer != null)
            {
                Transform _uiItemTransform = uiPlayer.transform;
                _uiItemTransform.SetParent(uiPlayer.currentParent);
                _uiItemTransform.localPosition = Vector3.zero;
            }
            
            uiItemTransform.SetParent(transform); 
            uiItemTransform.localPosition = Vector3.zero;

            uiPlayer = uiPlayer;
        }
    }
}