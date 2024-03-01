using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LocalPlayerController : NetworkBehaviour
{
    private bool isLocalPlayerInitialized = false;

    void Update()
    {
        if (!isLocalPlayerInitialized && isLocalPlayer)
        {
            // This is the local player, initialize it
            InitializeLocalPlayer();
            isLocalPlayerInitialized = true;
        }

        if (!isLocalPlayer)
            return;

        // Add your local player logic here
    }

    void InitializeLocalPlayer()
    {
        // Flip the appearance for the local player
        FlipAppearance();

        // You can perform any other local player initialization here
    }

    void FlipAppearance()
    {
        // Flip the appearance only for the local player
        // You may need to adjust the localScale values based on your game's setup
        if (transform.localScale.x > 0) // Not flipped
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip horizontally
        }
    }

    public override void OnStartLocalPlayer()
    {
        // This method is called only for the local player
        // You can perform any additional setup here if needed
    }
}