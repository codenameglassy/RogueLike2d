using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyEntityData", menuName = "EntityData/Enemy/EnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("Hurt")]
    public float hurtTime;
    public Material whiteMat;

    

    [Header("Attack")]
    public float attackDamage;
    public float attackRange;
    public float attackCoolDown;
    public LayerMask damageableLayer;

    [Header("Knockback")]
    public float knockbackForce = 8f;
    public float knockbackDuration = 0.15f;

}
