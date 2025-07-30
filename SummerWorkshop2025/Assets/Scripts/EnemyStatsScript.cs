using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatsScript : MonoBehaviour
{
    // The base stats of the entity.
    public HealthStatsScriptableObject baseStats;

    // The current health of the entity.
    public float health;

    // The current defense of the entity.
    public float defense;

    // Is the entity immune to damage?
    public bool isImmune;
}
