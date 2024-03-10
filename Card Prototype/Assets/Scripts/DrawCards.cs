using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    //Variables used for networking and determining game logic
    public PlayerManager PlayerManager;
    public GameManager GameManager;

    //Searches for object called GameManager and acquires the component with the same name
    private void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Runs code when a click made on the button
    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        //Checks current state of game and which version of Click to run
        if(GameManager.GameState == "Initialize {}")
        {
            InitializeClick();
        }
        else if (GameManager.GameState == "Compile {}")
        {
            CompileClick();
        }
        else if (GameManager.GameState == "Execute {}")
        {
            ExecuteClick();
        }
    }

    //Allows the button to pressed during the initial state of the game
    void InitializeClick()
    {
        PlayerManager.CmdDealCards();
        Debug.Log("Executing CmdDealCards");
    }

    //Prevents the button from being pressed during other states of the game
    void CompileClick()
    {

    }
    
    //Prevents the button from being pressed during other states of the game
    void ExecuteClick()
    {

    }
}
