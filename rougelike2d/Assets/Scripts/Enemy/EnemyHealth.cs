using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthComponent
{
  
    private bool isKnocked;

    [Header("Components")]
    public EnemyEntity entity;
    public Rigidbody2D rb;
    [SerializeField]private EnemyData data;

    [Header("HealthBar")]
    public Transform fill;

    public override void Start()
    {
        base.Start();
        SetCurrentHealth(data.maxHealth);
    }


    public override void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction)
    {
        base.RecieveDamage(attacker, damageAmt, direction);

        entity.stateMachine.ChangeState(entity.hurtState);

        sr.material = entity.data.whiteMat;
        Invoke("ResetMat", .14f);
        ApplyKnockback(direction);
        UpdateBar();

        if (GetCurrentHealth() <= 0)
        {
            EnemyKilled();
        }
        
    }

    void EnemyKilled()
    {
        Instantiate(data.deathVfx, transform.position, Quaternion.identity);
        GameManager.instance.RemoveEnemy(this.transform);
        XpManager.instance.AddXp(data.xpToGive);
        gameObject.SetActive(false);
    }

    public void ApplyKnockback(Vector2 attackerPosition)
    {
        if (isKnocked) return;

        Vector2 knockbackDir = (rb.position - attackerPosition).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackDir * data.knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        isKnocked = true;
        yield return new WaitForSeconds(data.knockbackDuration);
        rb.velocity = Vector2.zero;
        isKnocked = false;
    }

    void UpdateBar()
    {
        float percent = GetCurrentHealth() / data.maxHealth;
        fill.localScale = new Vector3(percent, 1f, 1f);
    }
}
