using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnailDefenseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("?");
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostTime = currentEnemy.maxTime;
        currentEnemy.anim.SetBool("defense", true);
    }
    public override void LogicUpdate()
    {
        currentEnemy.gameObject.layer = LayerMask.NameToLayer("Default");
        if (!currentEnemy.FoundPlayer())
        {
            currentEnemy.TimeCounter();
        }
    }

    public override void PhycicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.gameObject.layer = LayerMask.NameToLayer("Enemy");
        currentEnemy.anim.SetBool("defense", false);
    }
}
