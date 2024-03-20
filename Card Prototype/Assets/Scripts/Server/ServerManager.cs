using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    private List<Card> cardPool = new List<Card>();

    // List of players connected to the server
    private List<NetworkConnectionToClient> connections = new List<NetworkConnectionToClient>();

    // List of cards in each player's hand
    private Dictionary<NetworkConnectionToClient, List<Card>> playerHands = new Dictionary<NetworkConnectionToClient, List<Card>>();

    // List of cards on the game board
    private List<Card> gameBoard = new List<Card>();

    // List of cards in the graveyard
    private List<Card> graveyard = new List<Card>();

    // Number assigned to each player
    private Dictionary<NetworkConnectionToClient, int> playerNumbers = new Dictionary<NetworkConnectionToClient, int>();

    // Start is called before the first frame update
    void Start()
    {
        // Start server when the script starts
        StartServer();
    }

    // Method to start the server
    public void StartServer()
    {
        // Start the Mirror server
        NetworkManager.singleton.StartServer();

        // Log server start
        Debug.Log("Server started");
    }

    // Method to stop the server
    public void StopServer()
    {
        // Stop the Mirror server
        NetworkManager.singleton.StopServer();

        // Log server stop
        Debug.Log("Server stopped");
    }

    // Method to handle a player connecting to the server
    [Server]
    public void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Add the connection to the list of connections
        connections.Add(conn);

        // Initialize the player's hand as an empty list
        playerHands[conn] = new List<Card>();

        // Log the player connection
        Debug.Log("Player connected: " + conn.identity.name);

        // Assign a player number to the connected player
        AssignPlayerNumber(conn);

        // Add logic to initialize player's hand, draw cards, etc.
    }

    // Method to handle a player disconnecting from the server
    [Server]
    public void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // Remove the connection from the list of connections
        connections.Remove(conn);

        // Retrieve the player number and remove it from the dictionary
        int playerNumber = playerNumbers[conn];
        playerNumbers.Remove(conn);

        // Log the player disconnection
        Debug.Log("Player disconnected: " + conn.identity.name + " (Player " + playerNumber + ")");

        // Add logic to handle cleanup, end game if necessary, etc.
    }

    // Method to assign player number
    private void AssignPlayerNumber(NetworkConnectionToClient conn)
    {
        // Calculate player number based on the order of connections
        int playerNumber = connections.IndexOf(conn) + 1;

        // Assign player number
        playerNumbers[conn] = playerNumber;

        // Log the assignment of player number
        Debug.Log("Player " + conn.identity.name + " assigned number " + playerNumber);
    }

    // Method to handle game logic
    public void HandleGameLogic()
    {
        Debug.Log("Executing game logic...(TBD)");
        // Implement game logic here
        // This method could handle card interactions, turn management, etc.
    }
}
