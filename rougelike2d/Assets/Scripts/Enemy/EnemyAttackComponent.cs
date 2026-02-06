using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    [Header("Combat")]
    private Transform player;
    private float attackTimer;
    public Transform attackPoint;


    [SerializeField] private EnemyData data;


    private void Start()
    {
        player = GameManager.instance.player;
        attackTimer = data.attackCoolDown;
    }

    public void CheckPlayerInAttackRange()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= data.attackRange)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = data.attackCoolDown;
        }

        attackTimer -= Time.deltaTime;
    }

    void Attack()
    {
        Debug.Log("Enemy Attack!");

        // 🔥 Your attack logic here
        // damage player
        // play animation
        // enable hitbox

        Collider2D[] hitinfo = Physics2D.OverlapCircleAll(attackPoint.position, data.attackRange, data.damageableLayer);

        if (hitinfo.Length >= 1)
        {
            for (int i = 0; i < hitinfo.Length; i++)
            {
                hitinfo[i].GetComponent<IDamageable>().RecieveDamage(gameObject, data.attackDamage, transform.position);
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, data.attackRange);
    }
}
