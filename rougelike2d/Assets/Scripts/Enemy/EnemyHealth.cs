using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthComponent
{
    [Header("Knockback")]
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.15f;

    private bool isKnocked;

    public EnemyEntity entity;
    public Rigidbody2D rb;

    public GameObject deathVfx;
   
    public override void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction)
    {
        base.RecieveDamage(attacker, damageAmt, direction);

        entity.stateMachine.ChangeState(entity.hurtState);

        sr.material = entity.data.whiteMat;
        Invoke("ResetMat", .14f);
        ApplyKnockback(direction);

        if(GetCurrentHealth() <= 0)
        {
            Instantiate(deathVfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        
    }

    public void ApplyKnockback(Vector2 attackerPosition)
    {
        if (isKnocked) return;

        Vector2 knockbackDir = (rb.position - attackerPosition).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        isKnocked = true;
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        isKnocked = false;
    }
}
