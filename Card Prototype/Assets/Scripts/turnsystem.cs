using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public bool isYourTurn;
    public int yourTurn;
    public int isOpponentTurn;
    public Text turnText;
    public int maxMana;
    public int currentMana;
    public Text manaText;

    void Start()
    {
        isYourTurn = true;
        yourTurn = 1;
        isOpponentTurn = 0;

        maxMana = 1;
        currentMana = 1;
    }

    void Update()
    {
        if(isYourTurn == true)
        {
            turnText.text = "Your Turn";

        }
        else{
            turnText.text = "Opponent Turn";
        
        }
         manaText.text = currentMana + "/" + maxMana;
     }
  
    public void EndYourTurn()
    {
        isYourTurn = false;
        isOpponentTurn += 1;
    }

    public void EndOpponentTurn()
    {
        isYourTurn = true;
        yourTurn += 1;

        maxMana += 1;
        currentMana = maxMana;
    }
}