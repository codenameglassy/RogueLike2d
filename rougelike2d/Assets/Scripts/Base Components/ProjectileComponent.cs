using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    public ProjectileData data;
    private Vector2 direction;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void FixedUpdate()
    {
        transform.Translate(direction * data.projectileSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(transform.position, data.projectileHitRadius, data.damageableLayer);

        if(hitInfo != null)
        {
            IDamageable damageable = hitInfo.GetComponent<IDamageable>();

            if(damageable != null)
                damageable.RecieveDamage(gameObject, data.projectileDamage, transform.position);

            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, data.projectileHitRadius);
    }
}
