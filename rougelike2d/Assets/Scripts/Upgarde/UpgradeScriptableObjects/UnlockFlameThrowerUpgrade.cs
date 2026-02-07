using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Unlock FlameThrower")]
public class UnlockFlameThrowerUpgrade : UpgradeSO
{
    public override void Apply(GameObject player)
    {
        PlayerStats.instance.UnlockFlameThrower();
        SoundManager.Instance.Play("incendiary");
    }
}
