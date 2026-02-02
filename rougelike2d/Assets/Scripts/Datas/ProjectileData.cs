using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    [Header("Basic")]
    public float projectileSpeed = 10f;
    public LayerMask damageableLayer;
    public float projectileHitRadius = 0.25f;
    public float projectileDamage = 5f;

    [Header("Homing Missile")]
    public float rotateSpeed = 200f; 
}
