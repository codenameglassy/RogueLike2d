using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public Animator anim;
    public EnemyData data;
    public Rigidbody2D rb;

    public EnemyFiniteStateMachine stateMachine { get; private set; }

    public EnemyIdleState idleState { get; private set; }
    public EnemyHurtState hurtState { get; private set; }

    public Transform player;
    public float moveSpeed = 3f;

    public EnemyAttackComponent attackComponent;

    private void Awake()
    {
        stateMachine = new EnemyFiniteStateMachine();
    }
    private void Start()
    {
        idleState = new EnemyIdleState(this, stateMachine, "Idle", data);
        hurtState = new EnemyHurtState(this, stateMachine, "Hurt", data);

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

    public void ChasePlayer()
    {
        if (!player) return;

        Vector2 moveDir = (player.position - transform.position).normalized;

        CheckMovementDirection(moveDir);

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
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
        transform.localScale = scale;
    }


}
