using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BeeChaseState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        Debug.Log("!");
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostTime = currentEnemy.maxTime;
    }
    public override void LogicUpdate()
    {
        Vector3 direction = (currentEnemy.Player.transform.position - currentEnemy.transform.position).normalized;
        currentEnemy.rb.velocity = direction * currentEnemy.currentSpeed;
        if (!currentEnemy.FoundPlayer() || currentEnemy.ishurt)
        {
            currentEnemy.TimeCounter();
        }
        
    }


    public override void PhycicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }
}
