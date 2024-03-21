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

    [Command(requiresAuthority = false)]
    public void CmdEndTurn(NetworkConnectionToClient connection = null)
    {
        EndTurn(connection);
    }

    [Server]
    private void EndTurn(NetworkConnectionToClient connection)
    {
        if (!turnManager.IsCurrentTurn(connection)) return;
    //code that handles your turn end
    //
    }
}
