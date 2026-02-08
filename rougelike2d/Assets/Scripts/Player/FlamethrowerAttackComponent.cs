using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FlamethrowerAttackComponent : MonoBehaviour
{
    [Header("Box Settings")]
    [SerializeField] private Vector2 boxSize = new Vector2(2f, 2f);
    [SerializeField] private Vector2 boxOffset;
    [SerializeField] private bool useFacingDirection = true;

    [Header("Detection")]
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private int maxResults = 20;

    private Collider2D[] results;
    private Vector2 lastFacingDir = Vector2.right;

    private void Awake()
    {
        results = new Collider2D[maxResults];

        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        InvokeRepeating("PerformAttack", 1f, .5f);
    }

    /// <summary>
    /// Call this when you want to update facing direction (movement / aim)
    /// </summary>
    public void SetFacingDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.01f)
            lastFacingDir = dir.normalized;
    }

    /// <summary>
    /// Perform box check and return number of hits
    /// </summary>
    public int CheckBox(out Collider2D[] hits)
    {
        Vector2 center = (Vector2)transform.position + GetOffset();

        int hitCount = Physics2D.OverlapBoxNonAlloc(
            center,
            boxSize,
            0f,
            results,
            targetLayer
        );

        hits = results;
        return hitCount;
    }

    private Vector2 GetOffset()
    {
        if (!useFacingDirection)
            return boxOffset;

        return new Vector2(
            boxOffset.x * Mathf.Sign(lastFacingDir.x),
            boxOffset.y
        );
    }

    public void PerformAttack()
    {
        int hitCount = CheckBox(out Collider2D[] hits);

        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].TryGetComponent(out IDamageable damageable))
            {
                damageable.RecieveDamage(gameObject, 5f, transform.position,false);
            }
        }
    }

    #region Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 center = Application.isPlaying
            ? (Vector2)transform.position + GetOffset()
            : (Vector2)transform.position + boxOffset;

        Gizmos.DrawWireCube(center, boxSize);
    }
    #endregion

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
