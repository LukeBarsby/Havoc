using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    float wantedLevel;
    float score;
    public float editorWantedLevel;
    bool dead;

    public void Start()
    {
        PlayerController.Instance.GameOver = false;
        PlayerController.Instance.timer = 60;
        PlayerController.Instance.isDead = false;
        PlayerController.Instance.startTimer = true;
    }

    void Update()
    {
        dead = PlayerController.Instance.isDead;
        if (PlayerController.Instance.GameOver)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        PlayerController.Instance.startTimer = false;
        if (dead)
        {
            UIController.Instance.TurnUIOn(UIController.UIEnum.UILosePanel);
        }
        else
        {
            UIController.Instance.TurnUIOn(UIController.UIEnum.UIWinPanel);
        }
    }
    public float ReturnWantedLevel()
    {
        return wantedLevel;
    }
}
