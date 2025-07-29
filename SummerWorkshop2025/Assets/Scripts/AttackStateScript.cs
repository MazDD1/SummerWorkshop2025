using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AttackStateScript : MonoBehaviour
{
    [SerializeField]
    public AttackStatsScriptableObject attackStats;
    public int attackPriority = 1;
}
