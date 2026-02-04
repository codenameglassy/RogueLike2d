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
    public Rigidbody2D rb;

    //[Header("Finite State Machine")]
    public PlayerFinisteStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerAttackState attackState { get; private set; }


    [Header("Attack Refernces")]
    public Transform attackVfxPoint;

    private void Awake()
    {
        stateMachine = new PlayerFinisteStateMachine();


        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        idleState = new PlayerIdleState(this, stateMachine, "Idle", data);
        attackState = new PlayerAttackState(this, stateMachine, "Attack", data);

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

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

        if(newGameState == GameState.Gameplay)
        {
            
        }
        else if(newGameState == GameState.Paused)
        {
            rb.velocity = Vector2.zero;
            stateMachine.ChangeState(idleState);
        } 
    }
   
}
