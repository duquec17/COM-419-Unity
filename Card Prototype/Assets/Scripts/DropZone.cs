using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class DropZone : NetworkBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null && CanDropCard(d))
        {

        }
    }

    public bool CanDropCard(Draggable draggable)
    {
        // Checks to see if the card can be dropped on this spot
        if (gameObject.CompareTag("AllyDropZone") && draggable.isOwned)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
