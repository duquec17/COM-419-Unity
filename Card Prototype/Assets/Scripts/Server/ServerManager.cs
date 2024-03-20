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

    // Method to initialize the server
    public void StartServer()
    {
        // Start the server logic here
    }

    // Method to handle a player connecting to the server
    public void OnPlayerConnect(Player player)
    {
        players.Add(player);
        playerHands[player] = new List<Card>();

        // Add logic to initialize player's hand, draw cards, etc.
    }

    // Method to handle a player disconnecting from the server
    public void OnPlayerDisconnect(Player player)
    {
        players.Remove(player);
        // Add logic to handle cleanup, end game if necessary, etc.
    }

    // Method to handle game logic
    public void HandleGameLogic()
    {
        // Implement game logic here
        // This method could handle card interactions, turn management, etc.
    }
}
