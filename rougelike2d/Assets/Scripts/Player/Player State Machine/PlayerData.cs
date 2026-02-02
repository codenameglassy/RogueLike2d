using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerEntityData", menuName = "EntityData/Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Attack")]
    public float attackTime;
    public float attackRange;
    public float attackDamage;

    public LayerMask damageableLayer;

    [Header("Vfx")]
    public GameObject attackVfx;
    public Material whiteMat;



}
