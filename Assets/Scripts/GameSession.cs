using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLife = 3;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] int Score = 0;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LivesText.text = playerLife.ToString();
        ScoreText.text = Score.ToString();
    }
    public void ScoreUpdate(int PointsToAdd)
    {
        Score += PointsToAdd;
        ScoreText.text = Score.ToString();
    }

    public void processPlayerDeath()
    {
        if (playerLife > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLife--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        LivesText.text = playerLife.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScreenPersist>().ResetScreenPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
