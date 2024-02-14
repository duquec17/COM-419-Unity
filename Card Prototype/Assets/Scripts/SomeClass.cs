using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeClass : MonoBehaviour
{
    void Start()
    {
        // Example usage:
        Card allyCard = CardDatabase.cardList[0]; // Assuming you want the first card in the list
        Card enemyCard = CardDatabase.cardList[1]; // Assuming you want the second card in the list

        CombatResult result = CombatResolver.ResolveCombat(allyCard, enemyCard);

        Debug.Log("Combat Result - Attacker Wins: " + result.AttackerWins + ", Damage Dealt: " + result.DamageDealt);
    }
}