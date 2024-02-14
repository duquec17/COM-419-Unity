using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public DropZone allyDropZone;
    public DropZone enemyDropZone;

    void Start()
    {
        // Assuming this script is attached to a GameObject in the scene,
        // you can get references to the DropZone components via the GameObjects' tags.
        allyDropZone = GameObject.FindGameObjectWithTag("AllyDropZone").GetComponent<DropZone>();
        enemyDropZone = GameObject.FindGameObjectWithTag("EnemyDropZone").GetComponent<DropZone>();

        // Start combat at the beginning of the turn
        StartCombat();
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
    }

    void ApplyCombatResult(CombatResult result)
    {
        // Implement logic to apply the combat result
    }
}
