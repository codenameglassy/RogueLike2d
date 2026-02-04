using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Upgrade Pool")]
public class UpgradePoolSO : ScriptableObject
{
    public List<UpgradeSO> upgrades;
}
