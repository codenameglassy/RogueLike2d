using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Increase AttackPower")]
public class IncreaseAttackPowerUpgade : UpgradeSO
{
    public int attackPowerIncrease = 10;

    public override void Apply(GameObject player)
    {
        PlayerStats.instance.IncreasePlayerAttackPower(attackPowerIncrease);
        SoundManager.Instance.Play("upgrade");
    }
}
