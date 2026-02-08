using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerComponent : MonoBehaviour
{
    public enum AttackDirection
    {
        HORIZONTAL, VERTICAL
    }
    public AttackDirection attackDirection;

    [Header("Horizontal")]
    public GameObject flameThrowerHorizontal;

    [Header("Vertical")]
    public GameObject flameThrowerVertical;

    private void Awake()
    {
        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    private void Start()
    {
        Initialize();
    }

    public void SwitchDirection()
    {
        switch (attackDirection)
        {
            case AttackDirection.HORIZONTAL:
                attackDirection = AttackDirection.VERTICAL;
                break;

            case AttackDirection.VERTICAL:
                attackDirection = AttackDirection.HORIZONTAL;
                break;
        }

        switch (attackDirection)
        {
            case AttackDirection.HORIZONTAL:
                flameThrowerVertical.SetActive(false);
                flameThrowerHorizontal.SetActive(true);
                break;

            case AttackDirection.VERTICAL:
                flameThrowerVertical.SetActive(true);
                flameThrowerHorizontal.SetActive(false);
                break;
        }
    }

    void Initialize()
    {
        attackDirection = AttackDirection.VERTICAL;
        SwitchDirection();
        StartCoroutine(SwitchDirectionRoutine());

    }

    IEnumerator SwitchDirectionRoutine()
    {
        yield return new WaitForSeconds(3f);
        SwitchDirection();
        SoundManager.Instance.Play("flameThrowerSwitchDirection");
        StartCoroutine(SwitchDirectionRoutine());
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
