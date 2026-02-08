using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class XpManager : MonoBehaviour
{
    public static XpManager instance;

    [Header("Level")]
    public int currentLevel = 1;
    public int maxLevel = 50;

    [Header("XP")]
    public float currentXpAmt = 0;
    public float maxXpAmt = 100f;
    public float xpIncreasePerLevel = 25f; // how much XP requirement grows per level

    [Header("UI Elements")]
    public Image fillImage;
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateXpBar();
    }

    void UpdateXpBar()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentXpAmt / maxXpAmt;
        }
    }

    public void AddXp(float xpAmt)
    {
        if (currentLevel >= maxLevel)
            return; // already max level

        currentXpAmt += xpAmt;

        while (currentXpAmt >= maxXpAmt && currentLevel < maxLevel)
        {
            currentXpAmt -= maxXpAmt;
            LevelUp();
        }

        UpdateXpBar();
        ScoreManager.instance.AddScore((int)xpAmt);
    }

    void LevelUp()
    {
        SoundManager.Instance.Play("levelup");

        currentLevel++;

        // increase budget for difficulty in milestones
        GameDirector.instance.OnLevelUp(currentLevel);

        // Update Level in UI
        levelText.text = "Level: " + currentLevel.ToString();

        // Increase XP needed for next level
        maxXpAmt += xpIncreasePerLevel;

        // Show upgrade UI
        UpgradeManager.instance.ShowUpgradeChoices();

        Debug.Log("Level Up! Current Level: " + currentLevel);

        // Reset xp amount
        currentXpAmt = 0;
    }
}
