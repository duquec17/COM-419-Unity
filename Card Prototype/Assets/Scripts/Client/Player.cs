using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    // References to game objects
    public GameObject PlayerCard;
    public TurnManager turnManager;

    public GameObject Hand;
    public GameObject Deck;

    public List<GameObject> DropZones = new List<GameObject>();

    // Variables for game state
    public int Health;
    public int CardsPlayed = 0;
    public bool IsMyTurn = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to game objects
        Hand = GameObject.Find("AllyHand");
        Deck = GameObject.Find("Deck");

        // Connect drop zones to the list
        DropZones.Add(GameObject.Find("AllyDropZone"));
        DropZones.Add(GameObject.Find("AllyDropZone (1)"));
        DropZones.Add(GameObject.Find("AllyDropZone (2)"));
        DropZones.Add(GameObject.Find("AllyDropZone (3)"));
        DropZones.Add(GameObject.Find("AllyDropZone (4)"));
        DropZones.Add(GameObject.Find("AllyDropZone (5)"));

        // Find a reference to the TurnManager
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        // Subscribe to the event triggered when the turn changes
        turnManager.nextPlayer.AddListener(ImPlayer);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        CmdRegisterPlayer();
    }

    [Command]
    void CmdRegisterPlayer(NetworkConnectionToClient sender = null)
    {
        turnManager.RegisterPlayer(sender);
    }

    // Event triggered when the turn changes
    public void ImPlayer(uint playerID)
    {
        // Check if it's this player's turn based on their network identity
        IsMyTurn = turnManager.IsCurrentTurn(connectionToClient);

        // Output debug message indicating whose turn it is
        if (IsMyTurn)
        {
            Debug.Log("It's my turn!");
        }
        else
        {
            Debug.Log("It's the other player's turn.");
        }
    }
}
