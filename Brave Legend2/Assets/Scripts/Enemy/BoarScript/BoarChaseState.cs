using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.lostTime = currentEnemy.maxTime;
        currentEnemy.anim.SetBool("isRun",true);

    }
    public override void LogicUpdate()
    {
        if (!currentEnemy.phycicsCheck.isGround || (currentEnemy.phycicsCheck.leftTouchWall && currentEnemy.faceDir.x < 0) || (currentEnemy.phycicsCheck.rightTouchWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.transform.localScale.x*-1,1,1);
        }
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
        currentEnemy.anim.SetBool("isRun", false);
        currentEnemy.lostTime = currentEnemy.maxTime;
    }

}
