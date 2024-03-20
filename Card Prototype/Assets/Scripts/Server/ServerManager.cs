using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    // Reference to the GameManager
    public GameManager gameManager;

    // Reference to the PlayerManager
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find references to GameManager and PlayerManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Method to compare power and health values of cards in the first drop zones of both players
    public void CompareCards()
    {

    }

    // Method to notify clients to update the displayed cards after combat
    [ClientRpc]
    private void RpcUpdateCombatResult(GameObject cardObject, int newHealth)
    {
        // Update the health of the card on the client side
        Card card = cardObject.GetComponent<Card>();
        card.health = newHealth;

        // Implement logic to update the UI or perform other actions based on the combat result
    }
}
