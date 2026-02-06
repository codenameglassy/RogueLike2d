using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Heal Player")]
public class HealPlayerUpgarde : UpgradeSO
{
    public float healAmt;
    public override void Apply(GameObject player)
    {
        PlayerStats.instance.HealPlayer(healAmt);
        SoundManager.Instance.Play("heal");
    }
}
