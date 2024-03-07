using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Canvas;
    public GameObject DropZone;
    public PlayerManager PlayerManager;
    

    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isDraggable = true;
    private GameObject dropZone;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        Debug.Log("Canvas var. is " + Canvas);
        DropZone = GameObject.Find("DropZone");
        Debug.Log("DropZone var. is " + DropZone);

        if (!isOwned)
        {
            isDraggable = false;
            Debug.Log("Isn't owned");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            isOverDropZone = true;
            dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }

    // Start is called before the first frame update
    public void StartDrag()
    {
        if (!isDraggable) return;

        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;

        isDragging = false;

        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
            Debug.Log("Can drop and IS my turn");
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
            Debug.Log("Can't drop and NOT my turn");
        }
    }   
}
