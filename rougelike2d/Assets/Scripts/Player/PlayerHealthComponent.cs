using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using DG.Tweening;

public class PlayerHealthComponent : HealthComponent
{
    [Header("UI Elements")]
    public Image fillImage;
    public CanvasGroup hurtImg;
    private bool isHurtImg = false;

    [Header("Components")]
    public PlayerEntity playerEntity;
    public MMF_Player hurtFeedback;
    public PlayerData data;

    public override void Start()
    {
        base.Start();
        SetCurrentHealth(playerEntity.data.maxHealth);
        UpdateHealthBar();
    }

    public override void RecieveDamage(GameObject attacker, float damageAmt, Vector2 direction)
    {
        base.RecieveDamage(attacker, damageAmt, direction);

        sr.material = playerEntity.data.whiteMat;
        Invoke("ResetMat", .14f);
        SoundManager.Instance.Play("hurt");

        if (!GameManager.instance.IsGamePaused())
        {
     
        }
        hurtFeedback.PlayFeedbacks();
        UpdateHealthBar();
        StartCoroutine(HurtImgRoutine());

        if(GetCurrentHealth() <= 0)
        {
            GameOver();
        }

    }

    public void Heal(float healAmt)
    {
        float currentHealth = GetCurrentHealth();
        float updatedHealth = currentHealth + healAmt;

        if(updatedHealth >= data.maxHealth)
        {
            updatedHealth = data.maxHealth;
        }

        SetCurrentHealth(updatedHealth);
        UpdateHealthBar();
        Debug.Log("Player healed by " + healAmt);
    }

    void UpdateHealthBar()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = GetCurrentHealth() / playerEntity.data.maxHealth;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        // Disable any upgrades
        GameManager.instance.DisableUpgradedItems();
        // Fade Out & Gameover
        GameManager.instance.DelayedGameover();
        // Pause Game
        GameManager.instance.PauseGame();
        // Disable Player
        gameObject.SetActive(false);
    }

    IEnumerator HurtImgRoutine()
    {
        if(isHurtImg == true)
        {
            yield break;
        }
        isHurtImg = true;

        hurtImg.alpha = 1.0f;
        hurtImg.DOFade(0.0f, .25f);
        yield return new WaitForSeconds(.5f);

        isHurtImg = false;
    }
}
