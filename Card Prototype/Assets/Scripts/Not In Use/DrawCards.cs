using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    [SerializeField] private TurnManager turnManager;

    private void Start()
    {
        // Find a reference to the TurnManager if not set
        if (turnManager == null)
            turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    // This method is called when the button is clicked
    public void OnClick()
    {
        // Ensure that this client has authority to execute the action
        if (!isOwned)
        {
            Debug.LogWarning("You don't have authority to perform this action.");
            return;
        }

        // Check if it's the current player's turn
        if (!IsPlayerTurn())
        {
            Debug.LogWarning("It's not your turn.");
            return;
        }

        // Call the command on the server to draw cards
        CmdDrawCards();
    }

    // Check if it's the current player's turn
    private bool IsPlayerTurn()
    {
        return turnManager.IsCurrentTurn(connectionToClient);
    }

    // Command to draw cards on the server
    [Command]
    private void CmdDrawCards()
    {
        // Ensure it's the current player's turn
        if (!IsPlayerTurn())
            return;

        // Perform draw cards logic here
        // For example:
        // draw cards code
    }
}
