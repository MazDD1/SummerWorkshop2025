using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Type", menuName = "ScriptableObjects/EnemyType")]

public class EnemyTypesSO : ScriptableObject
{
    public HealthStatsScriptableObject healthStats;
    public AttackStatsScriptableObject[] enemyAttacks;

    public string idleAnim;
}
