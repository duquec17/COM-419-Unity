using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatResult : MonoBehaviour
{
    public bool attackerWins;
    public int damageDealt;
    public CombatResult()
    {
    }
    public CombatResult(bool AttackerWins, int DamageDealt)
    {
        attackerWins = AttackerWins;
        damageDealt = DamageDealt;
    }
}
