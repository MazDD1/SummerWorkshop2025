using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationFunctionScripts : MonoBehaviour
{

    // This script exists so the Animation component can play the hit events.
    // All it does is call other events in the main logic, as the Animation component can only call functions in the GameObject it's in.

    [SerializeField]
    private TurnBasedLogic mainLogic;

    public void Player_Attack()
    {
        mainLogic.Player_Attack();
    }

    public void Enemy_Attack()
    {
        mainLogic.Enemy_Attack();
    }

    public void Animation_End()
    {
        mainLogic.Attack_End(this.gameObject);
    }
}
