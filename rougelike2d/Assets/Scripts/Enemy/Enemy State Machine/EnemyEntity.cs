using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public SpriteRenderer sr;
    public EnemyData data;
    public Rigidbody2D rb;
    public EnemyAttackComponent attackComponent;
    public EnemyFiniteStateMachine stateMachine { get; private set; }

    public EnemyIdleState idleState { get; private set; }
    public EnemyHurtState hurtState { get; private set; }


    private Transform player;

    private void Awake()
    {
       
        stateMachine = new EnemyFiniteStateMachine();

        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;

    }
    private void Start()
    {
        idleState = new EnemyIdleState(this, stateMachine, "Idle", data);
        hurtState = new EnemyHurtState(this, stateMachine, "Idle", data);

        stateMachine.Initialize(idleState);

        //init
        GameManager.instance.AddEnemy(this.transform);
        player = GameManager.instance.player;

    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void ChasePlayer()
    {
        if (!player) return;

        if (GameManager.instance.IsGamePaused())
            return;

        Vector2 moveDir = (player.position - transform.position).normalized;

        CheckMovementDirection(moveDir);

        rb.MovePosition(rb.position + moveDir * data.moveSpeed * Time.fixedDeltaTime);
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private bool isFacingRight = true;

    private void CheckMovementDirection(Vector2 moveDir)
    {
        if (moveDir.x > 0 && !isFacingRight)
            Flip();
        else if (moveDir.x < 0 && isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        //transform.localScale = scale;
        sr.flipX = isFacingRight;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }


}
