using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Unlock AirBlast")]
public class UnlockAirBlastUpgrade : UpgradeSO
{

    public override void Apply(GameObject player)
    {
        PlayerStats.instance.UnlockAirBlast();

        UpgradeManager.instance.RemoveUpgradeFromRunTimePool(this);
    }
}
