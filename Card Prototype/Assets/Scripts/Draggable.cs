using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        this.transform.SetParent(this.transform.parent.parent);

        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
