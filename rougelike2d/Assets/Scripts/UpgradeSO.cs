using UnityEngine;

public abstract class UpgradeSO : ScriptableObject
{
    [Header("UI")]
    public string upgradeName;
    [TextArea] public string description;
    public Sprite icon;

    public abstract void Apply(GameObject player);
}
