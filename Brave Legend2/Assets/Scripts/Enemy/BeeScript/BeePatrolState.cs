using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BeePatrolState : BaseState
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
        currentEnemy.rb.velocity =currentEnemy.flyRandomDir * currentEnemy.currentSpeed;
        float dist = Vector3.Distance(currentEnemy.opos, currentEnemy.transform.position);
        currentEnemy.timer += Time.deltaTime;
        if (currentEnemy.timer > 2)
        {
            currentEnemy.flyRandomDir= new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0);
            currentEnemy.timer = 0;
        }
        if (dist > currentEnemy.moveRadios)
        {
            currentEnemy.flyRandomDir = -currentEnemy.flyRandomDir;
        }
    }

    public override void PhycicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}
