using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerFinisteStateMachine stateMachine;
    protected PlayerEntity entity;
    protected PlayerData data;

    protected float startTime;
    protected string animBoolName;
    public PlayerState(PlayerEntity entity, PlayerFinisteStateMachine stateMachine, string animBoolName, PlayerData data)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.data = data;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }


}
