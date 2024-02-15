using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class TurnSystem : NetworkBehaviour
{
    public enum TurnPhase { Player, Opponent, Battle };

    [SyncVar(hook = nameof(OnCurrentPhaseChanged))]
    public TurnPhase currentPhase;
    [SyncVar]
    public int yourTurn;
    [SyncVar]
    public int isOpponentTurn;
    public Text turnText;
    public int maxMana;
    [SyncVar]
    public int currentMana;
    public Text manaText;
    internal static bool startTurn;
    private bool isPlayerFirst;

    // Define delegate for the event
    public delegate void PhaseChangeHandler(TurnPhase newPhase);

    // Define event based on the delegate
    public event PhaseChangeHandler OnPhaseChange;

    void Start()
    {
        yourTurn = 1;
        isOpponentTurn = 0;
        maxMana = 1;
        currentMana = 1;
        currentPhase = TurnPhase.Player;
        isPlayerFirst = true; // Assuming player goes first initially
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        switch (currentPhase)
        {
            case TurnPhase.Player:
                turnText.text = "Your Turn";
                break;
            case TurnPhase.Opponent:
                turnText.text = "Opponent Turn";
                break;
            case TurnPhase.Battle:
                turnText.text = "Battle Phase";
                break;
        }
        if (manaText != null)
            manaText.text = currentMana + "/" + maxMana;
    }

    public void EndYourTurn()
    {
        if (!isServer) return;

        if (currentPhase == TurnPhase.Player)
        {
            currentPhase = TurnPhase.Opponent;
            isOpponentTurn++;
            // Raise event when phase changes
            if (OnPhaseChange != null)
                OnPhaseChange(currentPhase);
        }
    }

    public void EndOpponentTurn()
    {
        if (!isServer) return;

        if (currentPhase == TurnPhase.Opponent)
        {
            currentPhase = TurnPhase.Battle;
            // Raise event when phase changes
            if (OnPhaseChange != null)
                OnPhaseChange(currentPhase);
        }
        else if (currentPhase == TurnPhase.Battle)
        {
            currentPhase = TurnPhase.Player;
            yourTurn++;

            maxMana++;
            currentMana = maxMana;

            if (!isPlayerFirst) // If the player didn't go first in the beginning
            {
                currentPhase = TurnPhase.Opponent; // Switch to opponent's turn
                isOpponentTurn++;
                // Raise event when phase changes
                if (OnPhaseChange != null)
                    OnPhaseChange(currentPhase);
            }
        }
    }

    public void SetPlayerFirst(bool isFirst)
    {
        isPlayerFirst = isFirst;
    }

    void OnCurrentPhaseChanged(TurnPhase oldPhase, TurnPhase newPhase)
    {
        // Handle UI or other logic when current phase changes
        // This hook function is called whenever the currentPhase SyncVar changes
    }
}