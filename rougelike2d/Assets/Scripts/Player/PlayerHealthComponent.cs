using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerHealthComponent : HealthComponent
{
    public PlayerEntity playerEntity;
    public MMF_Player hurtFeedback;
    public override void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction)
    {
        base.RecieveDamage(attacker, damageAmt, direction);

        sr.material = playerEntity.data.whiteMat;
        Invoke("ResetMat", .14f);
        hurtFeedback.PlayFeedbacks();
        
    }
}
