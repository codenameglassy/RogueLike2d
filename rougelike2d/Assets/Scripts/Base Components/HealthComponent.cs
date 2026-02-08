using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
   
    private float currentHealth;

    public SpriteRenderer sr;
    private Material defMat;

    // Start is called before the first frame update
    public virtual void Start()
    {
        defMat = sr.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction, bool isCrit = false)
    {
        currentHealth -= damageAmt;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    } 

    public virtual void ResetMat()
    {
        sr.material = defMat;
    }

    public void SetCurrentHealth(float _newHealth)
    {
        currentHealth = _newHealth;
    }

}
