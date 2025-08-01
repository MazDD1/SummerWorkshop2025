using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shell Type", menuName = "ScriptableObjects/ShellType")]
public class ShellTypeSO : ScriptableObject
{
    public string shellName;
    public Sprite shellImage;

    public float moveSpeed;

    public HealthStatsScriptableObject healthStats;
    public AttackStatsScriptableObject[] playerAttacks;
    public AttackStatsScriptableObject[] abilityAttacks;

    public Animation idleAnim;
    public Animation defendAnimStart;
    public Animation defendAnimEnd;

}
