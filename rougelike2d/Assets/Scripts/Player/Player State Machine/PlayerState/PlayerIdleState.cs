using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerEntity entity, PlayerFinisteStateMachine stateMachine, string animBoolName, PlayerData data) : 
        base(entity, stateMachine, animBoolName, data)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player Enter Idle State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        entity.topDownMovement.HandleInput();

        if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
        {
            entity.stateMachine.ChangeState(entity.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.topDownMovement.HandleMovement();
    }

   
}
