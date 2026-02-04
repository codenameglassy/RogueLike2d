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

    public EnemyRangeAttackState rangeAttackState { get; private set; }


    private Transform player;

    public enum EnemyType
    {
        Melee,
        Range
    }
    public EnemyType _enemyType;

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

        if(_enemyType == EnemyType.Range)
        {
            rangeAttackState = new EnemyRangeAttackState(this, stateMachine, "RangeAttack", data);
        }

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
        if (GameManager.instance.IsGamePaused()) return;

        Vector2 moveDir = (player.position - transform.position).normalized;

        // Flip sprite based on movement direction
        if (moveDir.x != 0)
            sr.flipX = moveDir.x < 0;

        rb.MovePosition(rb.position + moveDir * data.moveSpeed * Time.fixedDeltaTime);
    }

    public void ShootProjectile()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject proj = Instantiate(
            data.enemyProjctile,
            transform.position,
            Quaternion.identity
        );

        proj.GetComponent<EnemyProjectileComponent>().Init(dir);

    }

    public bool IsPlayerInRangeAttackRange()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(transform.position, data.rangeAttackRange, data.damageableLayer);

        return hitInfo;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, data.rangeAttackRange);
    }


}
