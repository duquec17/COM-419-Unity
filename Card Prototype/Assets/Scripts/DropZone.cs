using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
       
        if (d != null)
        {
            if (gameObject.CompareTag("AllyDropZone")) // Check if it's an ally dropzone and the card is an ally card
            {
                d.parentToReturnTo = this.transform;
                Debug.Log(d.gameObject.name + " was dropped on " + gameObject.name);
            }
            else
            {
                Debug.Log("Cannot drop " + d.gameObject.name + " on " + gameObject.name + ". Only ally cards can be placed here.");
            }
        }
    }
}
