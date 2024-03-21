using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    private List<Card> cardPool = new List<Card>();
    private List<Player> players = new List<Player>();
    private Dictionary<Player, List<Card>> playerHands = new Dictionary<Player, List<Card>>();
    private List<Card> gameBoard = new List<Card>();
    private List<Card> graveyard = new List<Card>();
    private Dictionary<Player, int> playerNumbers = new Dictionary<Player, int>();
    private int currentPlayerIndex = 0; // Index of the current player in the players list

    

    // Method to initialize the game
    [Command]
    private void InitializeGame()
    {
        // Initialize card pool, players, etc. (Add your initialization logic here)

        // Determine the first player
        DetermineFirstPlayer();
    }

    // Method to determine the first player
    private void DetermineFirstPlayer()
    {
        // Shuffle the list of players
        ShufflePlayers();

        // Set the first player as the current player
        currentPlayerIndex = 0;
        // Optionally, you can inform clients about the first player's turn here
        // RpcUpdateCurrentPlayerTurn(players[currentPlayerIndex].connectionToClient);
    }

    // Method to shuffle the list of players
    private void ShufflePlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player temp = players[i];
            int randomIndex = Random.Range(i, players.Count);
            players[i] = players[randomIndex];
            players[randomIndex] = temp;
        }
    }

    // Method to initialize the game

    // Method to handle end turn action

    // Method to move to the next player's turn

    // RPC method to update the current player's turn on all clients

    // Method to handle player actions (e.g., playing a card, attacking, etc.)
}
