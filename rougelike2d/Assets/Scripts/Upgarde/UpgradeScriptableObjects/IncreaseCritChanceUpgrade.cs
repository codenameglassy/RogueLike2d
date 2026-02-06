using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Increase CritChance")]
public class IncreaseCritChanceUpgrade : UpgradeSO
{
    public int cirtChangeIncrease = 10;

    public override void Apply(GameObject player)
    {
        PlayerStats.instance.IncreasePlayerCritChance(cirtChangeIncrease);
        SoundManager.Instance.Play("upgrade");

    }
}
