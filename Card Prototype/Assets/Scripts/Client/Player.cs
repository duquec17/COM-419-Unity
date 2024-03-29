using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    // References to game objects
    public GameObject PlayerCard;
    public TurnManager turnManager;
    public HandManager handManager;

    public GameObject Hand;
    public GameObject Deck;

    public List<GameObject> DropZones = new List<GameObject>();
    public List<Card> handCards = new List<Card>();
    public List<Card> EnemyCards = new List<Card>();

    // Variables for game state
    public string playerName;
    public int Health;
    public int Mana;
    public int CardsPlayed = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize references to game objects
        Hand = GameObject.Find("AllyHand");
        Deck = GameObject.Find("Deck");

        // Find a reference to the TurnManager
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        handManager = GameObject.Find("HandManager").GetComponent<HandManager>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        CmdRegisterPlayer(); // Command to register the player

    }

    [Command]
    public void CmdRegisterPlayer(NetworkConnectionToClient sender = null)
    {
        turnManager.RegisterPlayer(sender); // Register the player with the TurnManager
    }
}
