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
        
    }

    public void ChangeGameState(string stateRequest)
    {
        if (stateRequest == "Initialize {}")
        {
            ReadyClicks = 0;
            GameState = "Initialize {}";
        }
        else if (stateRequest == "Complie {}")
        {
            if (ReadyClicks == 1)
            {
                GameState = "Compile {}";
            }
        }
        else if (stateRequest == "Execute {}")
        {
            GameState = "Execute {}";
        }
    }
}