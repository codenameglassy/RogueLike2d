using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public List<Transform> enemyList = new List<Transform>();
    public Transform player;

    public List<GameObject> upgardedItemList = new List<GameObject>();

    [Header("UI Elements")]
    public CanvasGroup fadeImg;

    private bool isGamePaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        fadeImg.alpha = 1.0f;
        DelayedFadeIn();
        
    }


    public void AddEnemy(Transform enemy)
    {
        enemyList.Add(enemy);
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemyList.Remove(enemy);
    }

    // Returns a random enemy or null if the list is empty
    public Transform GetRandomEnemy()
    {
        if (enemyList.Count == 0)
            return null;

        int index = Random.Range(0, enemyList.Count);
        return enemyList[index];
    }

    #region Ui
    void DelayedFadeIn()
    {
        Invoke("FadeIn", .4f);
    }

    public void FadeIn()
    {
        // fade in
        fadeImg.alpha = 1.0f;
        fadeImg.DOFade(0.0f, 1f);
    }

    public void FadeOut()
    {
        fadeImg.DOFade(1.0f, 1.5f);

    }
    #endregion

    #region Upgardes

    public void AddUpgardedItem(GameObject _item)
    {
        upgardedItemList.Add(_item);
    }

    public void DisableUpgradedItems()
    {
        for (int i = 0; i < upgardedItemList.Count; i++)
        {
            upgardedItemList[i].SetActive(false);
        }
    }
    #endregion


    #region Game Play

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public void ResumeGame()
    {
        isGamePaused = false;

        GameState newGameState = GameState.Gameplay;
        GameStateManager.Instance.SetState(newGameState);
    }

    public void PauseGame()
    {
        isGamePaused = true;

        GameState newGameState = GameState.Paused;
        GameStateManager.Instance.SetState(newGameState);
    }

    #endregion
}
