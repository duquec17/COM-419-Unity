using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatResolver
{
    public static CombatResult ResolveCombat(Card allyCard, Card enemyCard)
    {
        // Calculate damage done by ally to enemy
        int allyDamage = CalculateDamage(allyCard.power, enemyCard.health);
        enemyCard.health -= allyDamage;

        // Calculate damage done by enemy to ally
        int enemyDamage = CalculateDamage(enemyCard.power, allyCard.health);
        allyCard.health -= enemyDamage;

        // Create CombatResult object with the combat outcome
        CombatResult result = new CombatResult
        {
            attackerWins = allyDamage > enemyDamage, // Check who did more damage
            damageDealt = Mathf.Max(allyDamage, enemyDamage) // Get the maximum damage dealt
        };

        return result;
    }

    private static int CalculateDamage(int attack, int defense)
    {
        // Calculate damage by subtracting defense from attack
        int damage = attack - defense;
        // Ensure damage is non-negative
        return Mathf.Max(0, damage);
    }
}
