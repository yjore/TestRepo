using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
    }
    public override void Move()
    {
    }
    public override bool FoundPlayer()
    {
        return Physics2D.OverlapCircle(transform.position,dectectRadius,attackerLayer);
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,dectectRadius);
    }
    public override void TimeCounter()
    {
        SwitchState(NPCstate.Patrol);
    }
    public override void Destroy()
    {
        base.Destroy();
    }
}
