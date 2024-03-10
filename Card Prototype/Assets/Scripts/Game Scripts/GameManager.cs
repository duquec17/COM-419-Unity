using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    //Variables managing game logic
    public int TurnOrder = 0;
    public string GameState = "Initialize {}";
    public int AllyMana = 0;
    public int EnemyMana = 0;
    public int AllyVariables = 0;
    public int EnemyVariables = 0;

    private int ReadyClicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Current gameState: " + GameState);
    }

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
            GameState = "Execute {}";
            Debug.Log("Execute gameState: " + GameState);
        }
        else
        {
            Debug.Log("ERROR state: " + GameState);
        }
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks++;
    }

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
