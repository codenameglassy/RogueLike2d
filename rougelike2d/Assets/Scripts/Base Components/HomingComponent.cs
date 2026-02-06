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
    
        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            IDamageable damageable = hitInfo[i].GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.RecieveDamage(gameObject, data.projectileDamage, transform.position);
                SoundManager.Instance.PlayOneShotLimited("missilehit", 4);
            }
            
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

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

    }
}
