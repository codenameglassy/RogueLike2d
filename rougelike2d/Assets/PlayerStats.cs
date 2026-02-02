using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private float currentPlayerAttackPower = 0;
    private float currentPlayerCritChange = 0;


    private void Awake()
    {
        instance = this;
    }


    public void IncreasePlayerAttackPower(float increaseAmt)
    {
        currentPlayerAttackPower += increaseAmt;
    }
    public void IncreasePlayerCritChance(float increaseAmt)
    {
        currentPlayerCritChange += increaseAmt;
    }
}
