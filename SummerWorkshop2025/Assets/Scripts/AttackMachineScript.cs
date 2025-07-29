using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMachineScript : MonoBehaviour
{
    [SerializeField]
    private GameObject currentState;
    [SerializeField]
    private AttackStateScript[] attackStates;
}
