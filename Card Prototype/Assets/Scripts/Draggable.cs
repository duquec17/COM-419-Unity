using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class Draggable : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public PlayerManager PlayerManager;
    public Transform parentToReturnTo = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");

        if (!isOwned) return;

            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isOwned) return;
        this.transform.position = eventData.position;  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isOwned) return;
        
       Debug.Log("EndDrag");
       this.transform.SetParent(parentToReturnTo);    
       GetComponent<CanvasGroup>().blocksRaycasts = true;
       NetworkIdentity networkIdentity = NetworkClient.connection.identity;
       PlayerManager = networkIdentity.GetComponent<PlayerManager>();
       PlayerManager.PlayCard(gameObject);
    }
}
