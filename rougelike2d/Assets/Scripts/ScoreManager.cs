using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int currentScore = 0;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText_Gameover;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    public void AddScore(int scoreAddAmt)
    {
        currentScore += scoreAddAmt;
        scoreText.text = "Score: " + currentScore.ToString();
        scoreText_Gameover.text = "Current Score: " + currentScore.ToString();
    }

    public int GetScore()
    {
        return currentScore;
    }
}
