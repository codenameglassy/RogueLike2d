using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerAttackComponent : MonoBehaviour
{
    public PlayerData data;
  
    [Header("Attack Positions")]
    public Transform attackPoint;
    public Transform attackPointUp;
    public Transform attackPointDown;
    

    [Header("Feedbacks")]
    public MMF_Player attackHitFeedback;

    public void Attack()
    {
        Collider2D[] hitinfo = Physics2D.OverlapCircleAll(attackPoint.position, data.attackRange, data.damageableLayer);
        Collider2D[] hitinfoUp = Physics2D.OverlapCircleAll(attackPointUp.position, data.attackRange, data.damageableLayer);
        Collider2D[] hitinfoDown = Physics2D.OverlapCircleAll(attackPointDown.position, data.attackRange, data.damageableLayer);

        if(hitinfo != null)
        {
            for (int i = 0; i < hitinfo.Length; i++)
            {
                hitinfo[i].GetComponent<IDamageable>().RecieveDamage(gameObject, data.attackDamage, transform.position);
            }
        }

        if (hitinfoUp != null)
        {
            for (int i = 0; i < hitinfoUp.Length; i++)
            {
                hitinfoUp[i].GetComponent<IDamageable>().RecieveDamage(gameObject, data.attackDamage, transform.position);
            }
        }

        if (hitinfoDown != null)
        {
            for (int i = 0; i < hitinfoDown.Length; i++)
            {
                hitinfoDown[i].GetComponent<IDamageable>().RecieveDamage(gameObject, data.attackDamage, transform.position);
            }
        }

        if (hitinfo.Length >= 1 ||
        hitinfoUp.Length >= 1 ||
        hitinfoDown.Length >= 1)
        {
            Debug.Log("Hit something");
            attackHitFeedback.PlayFeedbacks();
        }

      

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, data.attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, data.attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, data.attackRange);
    }
}
