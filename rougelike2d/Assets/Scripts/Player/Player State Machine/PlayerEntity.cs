using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [Header("Set References")]
    public PlayerTopDownMovement topDownMovement;
    public PlayerAttackComponent attackComponent;
    public Animator anim;
    public PlayerData data;

    //[Header("Finite State Machine")]
    public PlayerFinisteStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAttackState attackState { get; private set; }


    [Header("Attack Refernces")]
    public Transform attackVfxPoint;

    private void Awake()
    {
        stateMachine = new PlayerFinisteStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle", data);
        attackState = new PlayerAttackState(this, stateMachine, "attack", data);
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void SpawnAttackVfx()
    {
        Instantiate(data.attackVfx, attackVfxPoint.position, transform.rotation);
    }

   
}
