using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatResolver
{
    public static CombatResult ResolveCombat(Card attacker, Card defender)
    {
        // Implement combat resolution logic here
        // For example, compare attacker's power with defender's health, etc.
        // Determine the outcome and return a CombatResult object indicating the result

        // For demonstration purposes, let's assume a simple comparison of power
        if (attacker.power > defender.health)
        {
            Debug.Log("CombatResult");

            // Attacker wins
            return new CombatResult(true, defender.health - attacker.power);
        }
        else if (attacker.power < defender.health)
        {
            // Defender wins
            return new CombatResult(false, attacker.health - defender.power);
        }
        else
        {
            Debug.Log("CombatResult");

            // It's a draw
            return new CombatResult(false, 0);
        }
    }
}
