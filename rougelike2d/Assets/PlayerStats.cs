using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private float currentPlayerAttackPower = 0;
    private float currentPlayerCritChance = 0;

    public GameObject homingMissile;
    private bool hasPlayerAirBlastUpgrade = false;

    private void Awake()
    {
        instance = this;
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
    }
    public void IncreasePlayerCritChance(float increaseAmt)
    {
        currentPlayerCritChance += increaseAmt;
    }

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
