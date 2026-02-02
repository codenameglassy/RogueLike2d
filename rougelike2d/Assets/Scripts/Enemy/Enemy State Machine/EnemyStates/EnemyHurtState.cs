using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    public EnemyHurtState(EnemyEntity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, EnemyData data) : 
        base(entity, stateMachine, animBoolName, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enemy Entered Hurt state");
        entity.StopMovement();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time >= startTime + data.hurtTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
