using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    private List<Card> cardPool = new List<Card>();

    // List of players connected to the server
    private List<Player> players = new List<Player>();

    // List of cards in each player's hand
    private Dictionary<Player, List<Card>> playerHands = new Dictionary<Player, List<Card>>();

    // List of cards on the game board
    private List<Card> gameBoard = new List<Card>();

    // List of cards in the graveyard
    private List<Card> graveyard = new List<Card>();

    // Number assigned to each player
    private Dictionary<Player, int> playerNumbers = new Dictionary<Player, int>();

    // Variable to track whose turn it is
    private bool isMyTurn = false;

    public override void OnStartClient()
    {
        base.OnStartClient();

        GameObject playerObject = GameObject.FindWithTag("Player"); // Assuming players are tagged with "Player"
        Player playerComponent = playerObject.GetComponent<Player>();

        // Add the player to the list of connected players
        players.Add(playerComponent);

        // Assign a player number to the connected player
        AssignPlayerNumber(playerComponent);

        // If this is the first player, set their turn to true
        if (players.Count == 1)
        {
            isMyTurn = true;
        }

        // Add logic to initialize player's hand, draw cards, etc.
    }

    // Method to handle a player disconnecting from the server
    public void OnPlayerDisconnect(Player player)
    {
        // Remove the player from the list of connected players
        players.Remove(player);

        // Remove the player number entry
        playerNumbers.Remove(player);

        // Add logic to handle cleanup, end game if necessary, etc.
    }

    // Method to assign player number
    private void AssignPlayerNumber(Player player)
    {
        // Calculate player number based on the number of connected players
        int playerNumber = players.Count;

        // Assign player number
        playerNumbers[player] = playerNumber;

        // Log the assignment of player number
        Debug.Log("Player " + player.name + " assigned number " + playerNumber);
    }
}
