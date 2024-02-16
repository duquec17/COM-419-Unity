using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class Draggable : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Variable list
    public Transform parentToReturnTo = null;
    private Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isLocalPlayer)
            return;

        Debug.Log("BeginDrag");

        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLocalPlayer)
            return;

        this.transform.position = eventData.position;  
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocalPlayer)
            return;

        Debug.Log("EndDrag");
        this.transform.SetParent(parentToReturnTo);

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Call an RPC to synchronize the position of the card with all clients
        CmdUpdateCardPosition(this.transform.position);
    }

    [Command]
    private void CmdUpdateCardPosition(Vector3 newPosition)
    {
        RpcUpdateCardPosition(newPosition);
    }

    [ClientRpc]
    private void RpcUpdateCardPosition(Vector3 newPosition)
    {
        // Update the position of the card on all clients except the local player
        if (!isLocalPlayer)
            this.transform.position = newPosition;
    }
}
