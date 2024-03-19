using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CombatManager : NetworkBehaviour
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
        // Retrieve card information from the first drop zones of both players
        string allyCardInfo = GetCardInfo(playerManager.AllyDropZones[0]);
        string enemyCardInfo = GetCardInfo(playerManager.EnemyDropZones[0]);

        // Extract power and health values from the card information
        int allyPower = GetPowerValue(allyCardInfo);
        int allyHealth = GetHealthValue(allyCardInfo);
        int enemyPower = GetPowerValue(enemyCardInfo);
        int enemyHealth = GetHealthValue(enemyCardInfo);

        // Compare power and health values
        if (allyPower > enemyPower)
        {
            Debug.Log("Ally card has higher power than enemy card.");
            // Perform actions for Ally winning (e.g., deduct enemy health)
        }
        else if (allyPower < enemyPower)
        {
            Debug.Log("Enemy card has higher power than ally card.");
            // Perform actions for Enemy winning (e.g., deduct ally health)
        }
        else
        {
            Debug.Log("Both cards have equal power.");
            // Handle tie scenario (optional)
        }
    }

    // Helper method to retrieve card information from a drop zone
    private string GetCardInfo(GameObject dropZone)
    {
        Card card = dropZone.GetComponentInChildren<Card>();
        if (card != null)
        {
            return card.id.ToString() + " | Power: " + card.power + " | Health: " + card.health;
        }
        else
        {
            return "No card found";
        }
    }

    // Helper method to extract power value from card information string
    private int GetPowerValue(string cardInfo)
    {
        return 0;
    }

    // Helper method to extract health value from card information string
    private int GetHealthValue(string cardInfo)
    {
        
        return 0;
    }
}
