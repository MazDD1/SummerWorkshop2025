using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackStatsScriptableObject;

[CreateAssetMenu(fileName = "AttackStats", menuName = "ScriptableObjects/AttackStatsScriptableObject", order = 2)]
public class AttackStatsScriptableObject : ScriptableObject
{
    public enum StatChange {Health, Defense, NullifyDamage}
    public StatChange statChange;

    public enum AttackTarget {Player, Enemy}
    public AttackTarget attackTarget;


    // Subtract this number from the stat set in the statChange variable, negative numbers heal the stat. NullifyDamage does not use this variable.
    public float damage;
    public bool isPiercing;
    public AnimationClip attackAnimationName;

    public int attackPriority = 1;
    public string attackName;
    public string attackDescription;
}
