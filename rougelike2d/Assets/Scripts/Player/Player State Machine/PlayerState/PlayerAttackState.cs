using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerEntity entity, PlayerFinisteStateMachine stateMachine, string animBoolName, PlayerData data) : 
        base(entity, stateMachine, animBoolName, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player Enter Attack State");
        entity.topDownMovement.StopMovement();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime + data.attackTime)
        {
            entity.stateMachine.ChangeState(entity.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
