using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;

public class Draggable : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameManager GameManager;
    public PlayerManager PlayerManager;
    public Transform parentToReturnTo = null;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        NetworkIdentity netWorkIdentity = NetworkClient.connection.identity;
        PlayerManager = netWorkIdentity.GetComponent<PlayerManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");

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
        
            this.transform.SetParent(parentToReturnTo);    
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);

        Debug.Log("End Drag");
    }
}
