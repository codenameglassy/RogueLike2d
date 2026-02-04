using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject prefab;
    public int cost = 1;          // Budget cost
    public int weight = 10;       // Spawn chance
    public float minDifficulty;   // When this enemy can appear
}