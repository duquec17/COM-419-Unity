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

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if(GameManager.GameState == "Initialize {}")
        {
            
        }
        else if (GameManager.GameState == "Execute {}")
        {

        }
       
    }

    void InitializeClick()
    {
        PlayerManager.CmdDealCards();
    }

    void ExecuteClick()
    {

    }
}
