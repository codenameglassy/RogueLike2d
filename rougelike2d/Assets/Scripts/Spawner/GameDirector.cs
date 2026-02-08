using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;

    [Header("Difficulty")]
    public float difficulty;
    public float baseDifficultyRamp = 1f;

    [Header("Spawn Budget")]
    public float baseBudgetPerSecond = 5f;
    public float maxBudget = 50f;

    [Header("Kill Scaling")]
    public float killBudgetMultiplier = 0.8f;
    public float killDifficultyMultiplier = 0.3f;

    private float currentBudget;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        float kps = KillTracker.instance.GetKillsPerSecond();

        // Difficulty scaling
        difficulty += Time.deltaTime * (baseDifficultyRamp + kps * killDifficultyMultiplier);

        // Budget scaling
        currentBudget += Time.deltaTime * (baseBudgetPerSecond + kps * killBudgetMultiplier);
        currentBudget = Mathf.Min(currentBudget, maxBudget);
    }

    public bool CanSpend(int cost) => currentBudget >= cost;

    public void Spend(int cost) => currentBudget -= cost;

    public void OnLevelUp(int level)
    {
        if (level == 20) baseBudgetPerSecond = 3f;
        if (level == 40) baseBudgetPerSecond = 4f;
        if (level == 80) baseBudgetPerSecond = 5f;
        if (level == 120) baseBudgetPerSecond = 6f;

    }
}
