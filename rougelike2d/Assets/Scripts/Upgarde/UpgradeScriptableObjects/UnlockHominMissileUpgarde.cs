using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Unlock HominMissile")]
public class UnlockHominMissileUpgarde : UpgradeSO
{

    public override void Apply(GameObject player)
    {
        PlayerStats.instance.UnlockHomingMissile();
        SoundManager.Instance.Play("missile");
    }
}
