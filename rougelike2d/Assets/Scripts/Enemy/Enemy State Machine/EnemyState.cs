using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyFiniteStateMachine stateMachine;
    protected EnemyEntity entity;
    protected EnemyData data;

    protected float startTime;
    protected string animBoolName;

    public EnemyState(EnemyEntity entity,EnemyFiniteStateMachine stateMachine, string animBoolName, EnemyData data)
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
