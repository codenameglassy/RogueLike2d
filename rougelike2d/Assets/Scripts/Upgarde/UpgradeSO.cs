using UnityEngine;

public enum UpgradeRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
}

public abstract class UpgradeSO : ScriptableObject
{
    [Header("UI")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    [Header("Rarity")]
    public UpgradeRarity rarity = UpgradeRarity.Common;

    public abstract void Apply(GameObject player);
}
