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
    private int _currentPlayerIndex = 0;

    // SyncVar to synchronize current player across network
    [SyncVar(hook = nameof(NextTurnEvent))] public uint currentPlayer = 0;

    // UnityEvent to invoke when switching to next player
    public UnityEvent<uint> nextPlayer;

    // UnityEvent to invoke when a player is registered
    public UnityEvent<NetworkIdentity, int> playerRegisteredEvent;
    
    private HandManager handManager;

    // Track the number of players that have joined

    private void Start()
    {
        // Set the name of the GameObject to TurnManager
        gameObject.name = "TurnManager";
        handManager = GameObject.Find("HandManager").GetComponent<HandManager>();
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
        if (_identities.Contains(connection.identity)) return;

        //Handle here if reconnects can happen
        Debug.LogFormat("Payer added: {0}", connection.identity);

        // Add the player's identity to the identities list
        _identities.Add(connection.identity);
        
        // Invoke the playerRegisteredEvent UnityEvent with the player's identity and turn
        playerRegisteredEvent?.Invoke(connection.identity, GetPlayerTurn(connection));

        // If all players have joined, print the current number of players and whose turn it is
        if (_identities.Count == 2)
        {
            Debug.Log("Both players have joined. Current number of players: " + _identities.Count);
        }
    }

    [Server]
    public void NextPlayer()
    {
        Debug.Log("Previous turn: " + currentPlayer + " player");

        // Move to the next player
        _currentPlayerIndex++;

        // If it exceeds the count of identities, loop back to the second player
        if (_currentPlayerIndex >= _identities.Count) _currentPlayerIndex = 0;

        // Set the current player to the next player's netId
        currentPlayer = _identities[_currentPlayerIndex].netId;

        Debug.LogFormat("Current turn: {0} player", _currentPlayerIndex);
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
