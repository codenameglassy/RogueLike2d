using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        instance = this;
    }

    public void ShowUpgradeChoices()
    {
        upgradePanel.SetActive(true);

        currentChoices = GetRandomUpgrades(2);

        choiceLeft.Setup(currentChoices[0], this);
        choiceRight.Setup(currentChoices[1], this);

        Time.timeScale = 0.1f;
    }

    public void SelectUpgrade(UpgradeSO upgrade)
    {
        upgrade.Apply(player);
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private List<UpgradeSO> GetRandomUpgrades(int count)
    {
        List<UpgradeSO> pool = new List<UpgradeSO>(upgradePool.upgrades);
        List<UpgradeSO> result = new List<UpgradeSO>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, pool.Count);
            result.Add(pool[index]);
            pool.RemoveAt(index); // prevents duplicates
        }

        return result;
    }
}
