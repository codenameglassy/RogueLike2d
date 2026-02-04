using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;

public class PlayerHealthComponent : HealthComponent
{
    [Header("UI Elements")]
    public Image fillImage;

    [Header("Components")]
    public PlayerEntity playerEntity;
    public MMF_Player hurtFeedback;

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

        if(GetCurrentHealth() <= 0)
        {
            GameOver();
        }

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
        // Fade Out
        GameManager.instance.FadeOut();
        // Pause Game
        GameManager.instance.PauseGame();
        // Disable Player
        gameObject.SetActive(false);
    }
}
