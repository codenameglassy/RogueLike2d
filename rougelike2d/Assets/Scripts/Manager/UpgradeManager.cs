using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("Data")]
    public UpgradePoolSO upgradePool;

    [Header("UI")]
    public GameObject upgradePanel;        // Your existing Canvas panel
    public UpgradeChoiceUI choiceLeft;
    public UpgradeChoiceUI choiceRight;

    [Header("Player")]
    public GameObject player;

    private List<UpgradeSO> currentChoices = new();

    [Header("Rarity Weights")]
    public float commonWeight = 1f;
    public float uncommonWeight = 0.6f;
    public float rareWeight = 0.3f;
    public float legendaryWeight = 0.1f;

    private List<UpgradeSO> runtimePool = new List<UpgradeSO>();
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        runtimePool = new List<UpgradeSO>(upgradePool.upgrades);
    }

    public void ShowUpgradeChoices()
    {
        upgradePanel.SetActive(true);

        currentChoices = GetRandomUpgrades(2);

        choiceLeft.Setup(currentChoices[0], this);
        choiceRight.Setup(currentChoices[1], this);

        // set game pause
        GameManager.instance.PauseGame();

        //Time.timeScale = 0f;
        
        
    }

    public void SelectUpgrade(UpgradeSO upgrade)
    {
        upgrade.Apply(player);
        upgradePanel.SetActive(false);
        //Time.timeScale = 1f;
        SoundManager.Instance.Play("button");
        // set game resume
        GameManager.instance.ResumeGame();
    }

    private List<UpgradeSO> GetRandomUpgrades(int count)
    {
        //List<UpgradeSO> pool = new List<UpgradeSO>(upgradePool.upgrades);

        List<UpgradeSO> pool = new List<UpgradeSO>(runtimePool);
        List<UpgradeSO> result = new List<UpgradeSO>();

        for (int i = 0; i < count; i++)
        {
            if (pool.Count == 0) break;

            UpgradeSO selected = GetWeightedRandomUpgrade(pool);
            result.Add(selected);
            pool.Remove(selected); // prevent duplicates
        }

        return result;
    }

    private UpgradeSO GetWeightedRandomUpgrade(List<UpgradeSO> pool)
    {
        float totalWeight = 0f;
        foreach (var u in pool)
            totalWeight += GetRarityWeight(u.rarity);

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        foreach (var u in pool)
        {
            cumulative += GetRarityWeight(u.rarity);
            if (randomValue <= cumulative)
                return u;
        }

        return pool[pool.Count - 1]; // fallback
    }

    private float GetRarityWeight(UpgradeRarity rarity)
    {
        return rarity switch
        {
            UpgradeRarity.Common => commonWeight,
            UpgradeRarity.Uncommon => uncommonWeight,
            UpgradeRarity.Rare => rareWeight,
            UpgradeRarity.Legendary => legendaryWeight,
            _ => 1f
        };
    }

    public void RemoveUpgradeFromRunTimePool(UpgradeSO _upgardeSo)
    {
        runtimePool.Remove(_upgardeSo);
    }
}
