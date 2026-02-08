using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerAttackComponent : MonoBehaviour
{
    public PlayerData data;
    public PlayerTopDownMovement movementComponent;
  
    [Header("Attack Positions")]
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public Transform attackPointUp;
    public Transform attackPointDown;
    
    [Header("Feedbacks")]
    public MMF_Player attackHitFeedback;

    [Header("ShootPoints")]
    public List<Transform> shootPoints = new List<Transform>();
    private void Awake()
    {

        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;

    }

    public void Attack()
    {
        Collider2D[] hitinfoRight = Physics2D.OverlapCircleAll(attackPointRight.position, data.attackRange, data.damageableLayer);
        Collider2D[] hitinfoLeft = Physics2D.OverlapCircleAll(attackPointLeft.position, data.attackRange, data.damageableLayer);
        Collider2D[] hitinfoUp = Physics2D.OverlapCircleAll(attackPointUp.position, data.attackRange, data.damageableLayer);
        Collider2D[] hitinfoDown = Physics2D.OverlapCircleAll(attackPointDown.position, data.attackRange, data.damageableLayer);

        float currentDamage = data.attackDamage + PlayerStats.instance.GetPlayerAttackPower();
       
        Debug.Log(currentDamage + " Dealing damage.");

        bool isCrit = Random.value < (PlayerStats.instance.GetPlayerCritChance() / 100f);
        if (isCrit)
        {
            currentDamage = CalculateCritDamage((int)currentDamage);
        }

        switch (movementComponent.IsFacingRight())
        {
            case true:
                if (hitinfoRight != null)
                {
                    for (int i = 0; i < hitinfoRight.Length; i++)
                    {
                        hitinfoRight[i].GetComponent<IDamageable>().RecieveDamage(gameObject, currentDamage, transform.position, isCrit);
                    }
                }
                break;

            case false:
                if (hitinfoLeft != null)
                {
                    for (int i = 0; i < hitinfoLeft.Length; i++)
                    {
                        hitinfoLeft[i].GetComponent<IDamageable>().RecieveDamage(gameObject, currentDamage, transform.position, isCrit);
                    }
                }

                break;
        }
    
        if (hitinfoUp != null)
        {
            for (int i = 0; i < hitinfoUp.Length; i++)
            {
                hitinfoUp[i].GetComponent<IDamageable>().RecieveDamage(gameObject, currentDamage, transform.position, isCrit);
            }
        }

        if (hitinfoDown != null)
        {
            for (int i = 0; i < hitinfoDown.Length; i++)
            {
                hitinfoDown[i].GetComponent<IDamageable>().RecieveDamage(gameObject, currentDamage, transform.position, isCrit);
            }
        }

        if (hitinfoRight.Length >= 1 ||
        hitinfoUp.Length >= 1 ||
        hitinfoDown.Length >= 1 ||
        hitinfoLeft.Length >= 1)
        {
            if (!GameManager.instance.IsGamePaused())
            {
                Debug.Log("Hit something");
                attackHitFeedback.PlayFeedbacks();
            }
                 
            
           
        }

        if (PlayerStats.instance.HasPlayerAirBlastUpgarde())
        {
            ShootAtAllDirection();
            //SoundManager.Instance.Play("airblast");
        }

    }

    public void ShootAtAllDirection()
    {
        for (int i = 0; i < shootPoints.Count; i++)
        {
            GameObject proj = Instantiate(data.basicProjectile, shootPoints[i].position, Quaternion.identity);

            switch (i)
            {
                case 0:
                    //north
                    proj.GetComponent<ProjectileComponent>().SetDirection(Vector2.up);
                    break;
                case 1:
                    //south
                    proj.GetComponent<ProjectileComponent>().SetDirection(Vector2.down);
                    break;
                case 2:
                    //east
                    proj.GetComponent<ProjectileComponent>().SetDirection(Vector2.right);
                    break;
                case 3:
                    //west
                    proj.GetComponent<ProjectileComponent>().SetDirection(Vector2.left);
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireSphere(attackPointUp.position, data.attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, data.attackRange);
       

        switch (movementComponent.IsFacingRight())
        {
            case true:
                Gizmos.DrawWireSphere(attackPointRight.position, data.attackRange);
                break;

            case false:
                Gizmos.DrawWireSphere(attackPointLeft.position, data.attackRange);
                break;
        }
    }

    // Cirt Hit

    private float critMultiplier = 2f;

    public int CalculateCritDamage(int baseDamage)
    { 
        Debug.Log("CRITICAL HIT!");
        return Mathf.RoundToInt(baseDamage * critMultiplier);
   
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;

    }
}
