using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction, bool isCrit);

}
