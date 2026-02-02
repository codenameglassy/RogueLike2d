using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackComponent : MonoBehaviour
{
    [Header("Combat")]
    public float attackDamage;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.2f;
    public Transform player;
    private float attackTimer;
    public Transform attackPoint;
    public LayerMask damageableLayer;

    private void Start()
    {
        attackTimer = attackCooldown;
    }

    public void CheckPlayerInAttackRange()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
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

        Collider2D[] hitinfo = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageableLayer);

        if (hitinfo.Length >= 1)
        {
            for (int i = 0; i < hitinfo.Length; i++)
            {
                hitinfo[i].GetComponent<IDamageable>().RecieveDamage(gameObject, attackDamage, transform.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
