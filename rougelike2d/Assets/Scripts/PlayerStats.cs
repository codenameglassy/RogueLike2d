using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private float currentPlayerAttackPower = 0;
    private float currentPlayerCritChance = 0;

    [Header("Upgarde Prefabs")]
    public GameObject homingMissile;
    private bool hasPlayerAirBlastUpgrade = false;

    [Header("UI-Elements")]
    public TextMeshProUGUI apText;
    public TextMeshProUGUI critText;

    [Header("Player Component Reference")]
    public PlayerHealthComponent playerHealthComponent;
    public GameObject flamethrowerComponent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UnlockHomingMissile()
    {
        Instantiate(homingMissile, transform.position, Quaternion.identity);
    }
    public void UnlockAirBlast()
    {
        hasPlayerAirBlastUpgrade = true;
    }

    public void IncreasePlayerAttackPower(float increaseAmt)
    {
        currentPlayerAttackPower += increaseAmt;
        UpdateUI();
    }
    public void IncreasePlayerCritChance(float increaseAmt)
    {
        currentPlayerCritChance += increaseAmt;
        UpdateUI();
    }

    public void HealPlayer(float _healAmt)
    {
        playerHealthComponent.Heal(_healAmt);
    }

    public void UnlockFlameThrower()
    {
        flamethrowerComponent.SetActive(true);
    }

    #region UI

    public void UpdateUI()
    {
        apText.text = "AP: " + currentPlayerAttackPower;
        critText.text = "Crit-Chance: " + currentPlayerCritChance + "%";
    }
    #endregion

    #region Value Getter

    public bool HasPlayerAirBlastUpgarde()
    {
        return hasPlayerAirBlastUpgrade;
    }

    public float GetPlayerAttackPower()
    {
        return currentPlayerAttackPower;
    }

    public float GetPlayerCritChance()
    {
        return currentPlayerCritChance;
    }

    #endregion
}
