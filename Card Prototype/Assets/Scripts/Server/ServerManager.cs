using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ServerManager : NetworkBehaviour
{
    // SyncVars for game state variables
    [SyncVar]
    private List<Player> players = new List<Player>();

    [SyncVar]
    public int playerNum = 0;

    //Reference to the Player
    public Player player;

    // Reference to the TurnManager
    public TurnManager turnManager;

    // Other game state variables


    //Starts server (Host + Client)
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        //Tells us that the server is running
        Debug.Log("OnStartServer was activated");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find a reference to the TurnManager
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    private void Update()
    {
        // Check for the "N" key press
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Call the TestEndTurn method to manually change the current player's turn
            TestEndTurn();
        }
    }

    //Runs whens Server stops running
    [Server]
    public override void OnStopServer()
    {
        base.OnStopServer();

        //Tells us that the server is running
        Debug.Log("OnStartServer was deactivated");
    }

    //Starts when client button pressed
    public override void OnStartClient()
    {
        base.OnStartClient();
        //Message to show that client is running; appears in console tab
        Debug.Log("OnStartClient was activated");

    }

    //Runs when Client disconnects or leaves
    public override void OnStopClient()
    {
        base.OnStopClient();
        //Message to show that client is not running; appears in console tab
        Debug.Log("OnStartClient was deactivated");
    }

    // Method to test the EndTurn functionality
    public void TestEndTurn()
    {
        // Check if the TurnManager reference is valid
        if (turnManager != null)
        {
            // Call the EndTurn method on the TurnManager
            turnManager.NextPlayer();
        }
        else
        {
            Debug.LogError("TurnManager reference is null. Make sure to assign it in the Inspector.");
        }
    }
}