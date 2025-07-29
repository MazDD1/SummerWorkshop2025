using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStats", menuName = "ScriptableObjects/HealthStatsScriptableObject", order = 1)]
public class HealthStatsScriptableObject : ScriptableObject
{
    public float maxHealth = 100;
    // Multiplies the attack damage by this number, a lower number means a higher defense.
    public float defenseMultiplier = 1;
}
