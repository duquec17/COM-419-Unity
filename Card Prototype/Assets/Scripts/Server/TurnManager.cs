using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class TurnManager : NetworkBehaviour
{
    List<NetworkIdentity> _identities = new List<NetworkIdentity>();
    private int _currentPlayerIndex = 1;
    [SyncVar(hook = nameof(NextTurnEvent))] public uint currentPlayer = 0;
    public UnityEvent<uint> nextPlayer;
    public UnityEvent<NetworkIdentity, int> playerRegisteredEvent;

    // Track the number of players that have joined
    private int playersJoined = 0;

    private void Start()
    {
        gameObject.name = "TurnManager";
        if (isServer) _identities.Add(null);
    }

    [Server]
    public void RegisterPlayer(NetworkConnectionToClient connection)
    {
        //Handle here if reconnects can happen
        Debug.LogFormat("Payer added: {0}", connection.identity);

        _identities.Add(connection.identity);
        playerRegisteredEvent?.Invoke(connection.identity, GetPlayerTurn(connection));

        // Increment the count of players that have joined
        playersJoined++;

        // If all players have joined, print the current number of players and whose turn it is
        if (playersJoined == 2)
        {
            Debug.LogFormat("Both players have joined. Current number of players: {0}", playersJoined);
            Debug.LogFormat("Current turn: {0}", currentPlayer + " player");
        }
    }

    [Server]
    public void NextPlayer()
    {
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= _identities.Count) _currentPlayerIndex = 1;
        currentPlayer = _identities[_currentPlayerIndex].netId;
    }

    [Server]
    public bool IsCurrentTurn(NetworkConnectionToClient connection)
    {
        if (_identities[_currentPlayerIndex] == connection.identity) return true;
        //Sent a message to the client the action is out of turn
        //_errorManager.TargetErrorMessage(connection, "Not your turn");
        return false;
    }

    [Server]
    public int GetCurrentPlayerIndex()
    {
        return _currentPlayerIndex;
    }

    [Server]
    public uint GetPlayerByIndex(int index)
    {
        return _identities[index].netId;
    }

    public int GetPlayerTurn(NetworkConnectionToClient connection)
    {
        return _identities.IndexOf(connection.identity);
    }

    void NextTurnEvent(uint oldPlayer, uint newPlayer)
    {
        nextPlayer?.Invoke(newPlayer);
    }
}
