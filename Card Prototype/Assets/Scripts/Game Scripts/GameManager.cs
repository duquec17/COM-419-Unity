using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    //Variables managing game logic
    public int TurnOrder = 0;
    public string GameState = "Initialize {}";
    public int MaxHealth = 20;
    public int MaxMana = 10;

    // Reference to the player manager scripts of both players
    public PlayerManager playerManagerAlly;
    public PlayerManager playerManagerEnemy;
    public int AllyVariables = 0;
    public int EnemyVariables = 0;
    
    private int ReadyClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current gameState: " + GameState);
    }

    //Checks and Switches which state the game is in
    public void ChangeGameState(string stateRequest)
    {

        if (stateRequest == "Initialize {}")
        {
            ReadyClicks = 0;
            GameState = "Initialize {}";
            Debug.Log("Initialize gameState: " + GameState);
        }
        else if (stateRequest == "Compile {}")
        {
            if (ReadyClicks == 1)
            {
                GameState = "Compile {}";
            }

            Debug.Log("Compile gameState: " + GameState);
        }
        else if (stateRequest == "Execute {}")
        {
            if (ReadyClicks == 2)
            {
                GameState = "Execute {}";
            }

            Debug.Log("Execute gameState: " + GameState);
        }
        else
        {
            Debug.Log("ERROR state: " + GameState);
        }
    }

    //Not using leave alone or find way to make use
    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

    //Not using leave alone
    public void CardPlayed()
    {
        TurnOrder++;
        Debug.Log("Current TurnOrder: " + TurnOrder);
        if (TurnOrder == 6)
        {
            ChangeGameState("Execute {}");
            Debug.Log("Current gameState: " + GameState);
        }
    }
}
