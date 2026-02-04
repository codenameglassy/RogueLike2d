using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    [Header("Spawn Area")]
    public float spawnRadius = 9f;
    public float minDistance = 4f;

    [Header("Enemies")]
    public List<EnemySpawnData> enemyPool;
    private void Awake()
    {
        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    void Update()
    {
        TrySpawn();
    }

    void TrySpawn()
    {
        EnemySpawnData enemy = GetEnemyByDifficulty();
        if (enemy == null) return;

        if (!GameDirector.instance.CanSpend(enemy.cost))
            return;

        Vector2 pos = GetSpawnPosition();
        Instantiate(enemy.prefab, pos, Quaternion.identity);

        GameDirector.instance.Spend(enemy.cost);
    }

    EnemySpawnData GetEnemyByDifficulty()
    {
        float diff = GameDirector.instance.difficulty;

        List<EnemySpawnData> valid = new List<EnemySpawnData>();
        int totalWeight = 0;

        foreach (var e in enemyPool)
        {
            if (diff >= e.minDifficulty)
            {
                valid.Add(e);
                totalWeight += e.weight;
            }
        }

        if (valid.Count == 0) return null;

        int rand = Random.Range(0, totalWeight);
        int cur = 0;

        foreach (var e in valid)
        {
            cur += e.weight;
            if (rand < cur)
                return e;
        }

        return valid[0];
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        float dist = Random.Range(minDistance, spawnRadius);
        return (Vector2)player.position + dir * dist;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
