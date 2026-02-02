using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingComponent : MonoBehaviour
{
    public ProjectileData data;
    private Transform target;

    Rigidbody2D rb;

    public float interval = 2f; // time in seconds
    private float timer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.instance.AddUpgardedItem(this.gameObject);
    }

    private void Update()
    {

        timer += Time.deltaTime; // count time since last frame

        if (timer >= interval)
        {
            timer = 0f; // reset timer
            CheckForEnemyHit();
            target = GameManager.instance.GetRandomEnemy();
        }
    }

    void CheckForEnemyHit()
    {
        Collider2D[] hitInfo = Physics2D.OverlapCircleAll(transform.position, data.projectileHitRadius, data.damageableLayer);

        for (int i = 0; i < hitInfo.Length; i++)
        {
            hitInfo[i].GetComponent<IDamageable>().RecieveDamage(gameObject, data.projectileDamage, transform.position);
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = GameManager.instance.GetRandomEnemy();
            return;
        } 

        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * data.rotateSpeed;
        rb.velocity = transform.up * data.projectileSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, data.projectileHitRadius);
    }
  
}
