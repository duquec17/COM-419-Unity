using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Import LINQ namespace

public class CombatSystem : MonoBehaviour
{
    public DropZone[] allyDropZones; // Array to hold ally drop zones
    public DropZone[] enemyDropZones; // Array to hold enemy drop zones
    public TurnSystem turnSystem;

    void Start()
    {
        // Find all ally and enemy drop zones in the scene and convert them to DropZone array
        allyDropZones = GameObject.FindGameObjectsWithTag("AllyDropZone").Select(obj => obj.GetComponent<DropZone>()).ToArray();
        enemyDropZones = GameObject.FindGameObjectsWithTag("EnemyDropZone").Select(obj => obj.GetComponent<DropZone>()).ToArray();

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
        Debug.Log("Combat begins");

        // Check if both ally and enemy drop zones are not empty
        bool allyHasCard = CheckDropZonesNotEmpty(allyDropZones);
        bool enemyHasCard = CheckDropZonesNotEmpty(enemyDropZones);

        if (allyHasCard && enemyHasCard)
        {
            // Get cards from drop zones
            Card allyCard = GetRandomCardFromDropZones(allyDropZones);
            Card enemyCard = GetRandomCardFromDropZones(enemyDropZones);

            // Resolve combat between the two cards
            CombatResult result = CombatResolver.ResolveCombat(allyCard, enemyCard);

            // Apply the combat result
            ApplyCombatResult(result);
        }
        else if (allyHasCard || enemyHasCard)
        {

            Debug.Log("There is only one card on either drop zone");

            Debug.Log("Combat Ends");

            // If either drop zone is empty, end the turn
            turnSystem.EndOpponentTurn();
        }
    }

    bool CheckDropZonesNotEmpty(DropZone[] dropZones)
    {
        foreach (DropZone dropZone in dropZones)
        {
            if (!dropZone.HasCard())
            {
                return false;
            }
        }
        return true;
    }

    Card GetRandomCardFromDropZones(DropZone[] dropZones)
    {
        // Get a random drop zone
        DropZone dropZone = dropZones[Random.Range(0, dropZones.Length)];

        // Get the card from the drop zone
        return dropZone.GetCard();
    }

    void ApplyCombatResult(CombatResult result)
    {
        // Implement logic to apply the combat result

        // For demonstration purposes, let's just print the result
        Debug.Log("Combat Result: Damage Dealt: " + result.damageDealt);

        Debug.Log("Combat Ends");
        // After applying the combat result, end the turn
        turnSystem.EndOpponentTurn();
    }
}
