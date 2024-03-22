using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    //Variables used for networking and determining game logic
    public Player player;
    public TurnManager turnManager;
    public GameManager GameManager;
    public ServerManager serverManager;

    //Searches for object called GameManager and acquires the component with the same name
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        serverManager = GameObject.Find("ServerManager").GetComponent<ServerManager>();
    }

    [Command]
    // Runs code when a click is made on the button
    public void OnClick()
    {
        turnManager.NextPlayer();
    }
}
