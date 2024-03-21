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

    //Searches for object called GameManager and acquires the component with the same name
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
    }

    // Runs code when a click is made on the button
    public void OnClick()
    {

    }

    // Method to simulate drawing cards for the current player
    void DrawCardsForCurrentPlayer()
    {
        // Implement logic to draw cards here
    }
}
