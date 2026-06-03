using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailPatrolState : BaseState
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
            currentEnemy.transform.localScale = new Vector3(currentEnemy.transform.localScale.x * -1, 1, 1);
        }

    }

    public override void PhycicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }
}
