using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMachineScript : MonoBehaviour
{
    [SerializeField]
    public AttackStatsScriptableObject currentState;
    [SerializeField]
    public AttackStatsScriptableObject[] attackStates;
}
