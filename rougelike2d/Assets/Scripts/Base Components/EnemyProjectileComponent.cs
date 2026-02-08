using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileComponent : MonoBehaviour
{

    private Vector2 direction;

    public ProjectileData data;

    private void Awake()
    {

        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;

    }

    public void Init(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, 5f); // safety cleanup
    }

    void Update()
    {
        transform.position += (Vector3)(direction * data.projectileSpeed * Time.deltaTime);

        Collider2D hitInfo = Physics2D.OverlapCircle(transform.position, data.projectileHitRadius, data.damageableLayer);

        if (hitInfo != null)
        {
            IDamageable damageable = hitInfo.GetComponent<IDamageable>();

            if (damageable != null)
                damageable.RecieveDamage(gameObject, data.projectileDamage, transform.position, false);

            SoundManager.Instance.Play("projectilehit");
            Destroy(gameObject);
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

    }
}
