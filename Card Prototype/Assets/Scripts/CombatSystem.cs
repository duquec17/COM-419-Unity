using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public DropZone allyDropZone;
    public DropZone enemyDropZone;
    public TurnSystem turnSystem; // Reference to the TurnSystem script

    void Start()
    {
        // Get references to DropZone components
        allyDropZone = GameObject.FindGameObjectWithTag("AllyDropZone")?.GetComponent<DropZone>();
        enemyDropZone = GameObject.FindGameObjectWithTag("EnemyDropZone")?.GetComponent<DropZone>();

        // Get reference to the TurnSystem script
        turnSystem = FindObjectOfType<TurnSystem>();

        // Subscribe to the OnPhaseChange event of the TurnSystem
        turnSystem.OnPhaseChange += HandlePhaseChange;
    }

    void HandlePhaseChange(TurnSystem.TurnPhase newPhase)
    {
        // Check if the new phase is the battle phase
        if (newPhase == TurnSystem.TurnPhase.Battle)
        {
            // Start combat during the battle phase
            StartCombat();
        }
    }

    void StartCombat()
    {
        // Check if both drop zones are not empty
        if (allyDropZone.HasCard() && enemyDropZone.HasCard())
        {
            // Get the cards from both drop zones
            Card allyCard = allyDropZone.GetCard();
            Card enemyCard = enemyDropZone.GetCard();

            // Resolve combat between the two cards
            CombatResult result = CombatResolver.ResolveCombat(allyCard, enemyCard);

            // Apply the combat result
            ApplyCombatResult(result);
        }
        else
        {
            // If either drop zone is empty, end the turn
            turnSystem.EndOpponentTurn();
        }
    }

    void ApplyCombatResult(CombatResult result)
    {
        // Implement logic to apply the combat result

        // For demonstration purposes, let's just print the result
        Debug.Log("Combat Result - Attacker Wins: " + result.attackerWins + ", Damage Dealt: " + result.damageDealt);

        // After applying the combat result, end the turn
        turnSystem.EndOpponentTurn();
    }
}
