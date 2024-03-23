using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class TurnManager : NetworkBehaviour
{
    // List to hold the identities of players
    List<NetworkIdentity> _identities = new List<NetworkIdentity>();

    // Index of the current player
    private int _currentPlayerIndex = 1;

    // SyncVar to synchronize current player across network
    [SyncVar(hook = nameof(NextTurnEvent))] public uint currentPlayer = 0;

    // UnityEvent to invoke when switching to next player
    public UnityEvent<uint> nextPlayer;

    // UnityEvent to invoke when a player is registered
    public UnityEvent<NetworkIdentity, int> playerRegisteredEvent;
    
    private HandManager handManager;

    // Track the number of players that have joined
    public int playersJoined = 0;

    private void Start()
    {
        // Set the name of the GameObject to TurnManager
        gameObject.name = "TurnManager";
        handManager = GameObject.Find("HandManager").GetComponent<HandManager>();

        // If this instance is a server, add a null entry to the identities list
        if (isServer) _identities.Add(null);

    }

    private void Update()
    {
        // Check for the "X" key press
        if (Input.GetKeyDown(KeyCode.X))
        {
            CurrentList();
        }
    }

    [Server]
    public void RegisterPlayer(NetworkConnectionToClient connection)
    {
        //Handle here if reconnects can happen
        Debug.LogFormat("Payer added: {0}", connection.identity);

        // Add the player's identity to the identities list
        _identities.Add(connection.identity);
        
        // Invoke the playerRegisteredEvent UnityEvent with the player's identity and turn
        playerRegisteredEvent?.Invoke(connection.identity, GetPlayerTurn(connection));

        // Increment the count of players that have joined
        playersJoined++;

        // If all players have joined, print the current number of players and whose turn it is
        if (playersJoined == 2)
        {
            Debug.LogFormat("Both players have joined. Current number of players: {0}", playersJoined);
        }
    }

    [Server]
    public void NextPlayer()
    {
        // Log the cards in each player's hand
        foreach (NetworkIdentity identity in _identities)
        {
            HandManager handManager = identity.GetComponent<HandManager>();
            
            if (identity != null)
            {
                if (handManager != null)
                {
                    string playerName = identity.name;
                    string playerCards = "";
                    foreach (int cardId in handManager.handCardIds)
                    {
                        playerCards += cardId.ToString() + ", ";
                    }
                    Debug.LogFormat("{0} hand cards: {1}", playerName, playerCards);
                }
                else
                {
                    Debug.LogWarning("HandManager component not found on " + identity.name);
                }
            }
        }

        // Move to the next player
        _currentPlayerIndex++;
       
        // If it exceeds the count of identities, loop back to the second player
        if (_currentPlayerIndex >= _identities.Count) _currentPlayerIndex = 1;
        
        // Set the current player to the next player's netId
        currentPlayer = _identities[_currentPlayerIndex].netId;

        Debug.LogFormat("Current turn: {0} player", currentPlayer);
        Debug.Log(currentPlayer + " is the current player");
    }

    [Server]
    public bool IsCurrentTurn(NetworkConnectionToClient connection)
    {
        // Check if the provided connection's identity matches the current player's identity
        if (_identities[_currentPlayerIndex] == connection.identity) return true;
        //Sent a message to the client the action is out of turn
        Debug.Log("Action attempted out of turn.");
        return false;
    }

    [Server]
    public int GetCurrentPlayerIndex()
    {
        // Return the index of the current player
        return _currentPlayerIndex;
    }

    [Server]
    public uint GetPlayerByIndex(int index)
    {
        // Return the netId of the player at the specified index
        return _identities[index].netId;
    }

    public int GetPlayerTurn(NetworkConnectionToClient connection)
    {
        // Return the turn of the player associated with the given connection
        return _identities.IndexOf(connection.identity);
    }

    void NextTurnEvent(uint oldPlayer, uint newPlayer)
    {
        // Invoke the nextPlayer UnityEvent with the new player's netId
        nextPlayer?.Invoke(newPlayer);
    }

    [Server]
    // Function to output the current _identities list
    private void CurrentList()
    {
        // Output who is going first
        if (_identities.Count > 1 && _identities[1] != null)
        {
            Debug.LogFormat("Going first: {0}", _identities[1].name);
        }

        Debug.Log("Current _identities:");
        foreach (NetworkIdentity identity in _identities)
        {
            if (identity != null)
            {
                Debug.Log(identity.name);
            }
        }

        Debug.LogFormat("Current turn: {0}", _identities[_currentPlayerIndex].name);
    }
}
