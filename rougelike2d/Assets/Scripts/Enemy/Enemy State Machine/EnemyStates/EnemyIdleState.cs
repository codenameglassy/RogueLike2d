using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, EnemyData data) : 
        base(entity, stateMachine, animBoolName, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.ChasePlayer();
        entity.attackComponent.CheckPlayerInAttackRange();
    }
}
