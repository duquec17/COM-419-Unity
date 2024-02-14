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

        d.parentToReturnTo = this.transform;
        Debug.Log(d.gameObject.name + " was dropped on " + gameObject.name);

        if (d != null)
        {
            if (gameObject.CompareTag("AllyDropZone")) // Check if it's an ally dropzone and the card is an ally card
            {
               
            }
            else
            {
                Debug.Log("Cannot drop " + d.gameObject.name + " on " + gameObject.name + ". Only ally cards can be placed here.");
            }
        }
    }

    public Card card;

    public bool HasCard()
    {
        return card != null;
    }

    public Card GetCard()
    {
        if (HasCard())
        {
            Card currentCard = card;
            card = null; // Remove the card from the drop zone
            return currentCard;
        }
        else
        {
            Debug.LogError("Trying to get a card from an empty drop zone!");
            return null;
        }

    }
}
