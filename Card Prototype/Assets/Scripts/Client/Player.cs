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

        // Connect drop zones to the list (you might want to assign these in the inspector)
        for (int i = 0; i < 6; i++)
        {
            DropZones.Add(GameObject.Find("AllyDropZone (" + i + ")"));
        }

        // Find a reference to the TurnManager
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        CmdRegisterPlayer();
    }

    [Command]
    public void CmdRegisterPlayer(NetworkConnectionToClient sender = null)
    {
        turnManager.RegisterPlayer(sender);
    }

}
