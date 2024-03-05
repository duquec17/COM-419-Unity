using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayer : NetworkBehaviour
{
    // Reference to the hand container and drop zones
    public Transform handContainer;
    public DropZone[] dropZones;

    // Reference to the Draggable script attached to cards
    private Draggable[] draggableCards;

    // Start is called before the first frame update
    void Start()
    {
        // Find all Draggable scripts attached to cards in the hand container
        draggableCards = handContainer.GetComponentsInChildren<Draggable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        // Check for player input or other local player actions
    }

    // Example method to handle card drop event
    public void OnCardDropped(Draggable card, DropZone dropZone)
    {
        
    }
}
