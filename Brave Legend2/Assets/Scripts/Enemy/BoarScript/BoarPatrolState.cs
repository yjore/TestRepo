using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }

    public override void LogicUpdate()
    {
        
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCstate.Chase);
        }
        if (!currentEnemy.phycicsCheck.isGround || (currentEnemy.phycicsCheck.leftTouchWall && currentEnemy.faceDir.x < 0) || (currentEnemy.phycicsCheck.rightTouchWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("isWalk", false);
        }
        else
        {
            currentEnemy.anim.SetBool("isWalk", true);
        }
    }

    public override void PhycicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        currentEnemy.anim.SetBool("isWalk", false);
    }
}
