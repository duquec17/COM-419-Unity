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
        // Get the card GameObjects from the first drop zones of both players
        GameObject allyCardObject = playerManager.AllyDropZone1?.transform?.GetChild(0)?.gameObject;
        GameObject enemyCardObject = playerManager.EnemyDropZone1?.transform?.GetChild(0)?.gameObject;

        // Check for null references
        if (allyCardObject == null || enemyCardObject == null)
        {
            Debug.LogError("One or both card GameObjects are null.");
            return; // Exit the method to prevent further execution
        }

        // Get the Card components attached to the card GameObjects
        Card allyCard = allyCardObject.GetComponent<Card>();
        Card enemyCard = enemyCardObject.GetComponent<Card>();

        // Check for null Card components
        if (allyCard == null || enemyCard == null)
        {
            Debug.LogError("One or both Card components are null.");
            return; // Exit the method to prevent further execution
        }

        // Display the initial health values before combat
        Debug.Log("Ally Card Initial Health: " + allyCard.health);
        Debug.Log("Enemy Card Initial Health: " + enemyCard.health);

        // Subtract the power of the enemy card from the health of the ally card
        allyCard.health -= enemyCard.power;

        // Display the updated health values after combat
        Debug.Log("Ally Card Updated Health: " + allyCard.health);
        Debug.Log("Enemy Card Health: " + enemyCard.health);

        // Check if the ally card's health is less than or equal to 0
        if (allyCard.health <= 0)
        {
            // Destroy the ally card GameObject if its health is <= 0
            NetworkServer.Destroy(allyCardObject);
        }

        // Notify clients to update the displayed cards after combat
        RpcUpdateCombatResult(allyCardObject, allyCard.health);
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
