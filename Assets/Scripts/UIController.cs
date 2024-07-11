using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    public enum UIEnum
    {
        UIPanel,
        UIWinPanel,
        UILosePanel,
        Canvas
    }

    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject UIPanel;
    [SerializeField] GameObject UIWinPanel;
    [SerializeField] GameObject UILosePanel;

    [SerializeField] Text winScore;
    [SerializeField] Text loseScore;
    
    [SerializeField] Text score;
    [SerializeField] Text time;


    public GameObject weaponIconHolder;
    public GameObject wantedIconHolder;
    public GameObject healthIconHolder;
    public Sprite[] weaponICons;
    public Sprite[] wantedIcon;
    string icon;
    float wantedLevel;
    float healthBar;
    bool UITurnedOn;

    void Start()
    {
        GetCurrentWeaponIcon();
        UITurnedOn = false;
        UIPanel.SetActive(false);
        UIWinPanel.SetActive(false);
        UILosePanel.SetActive(false);
    }

    void Update()
    {
        if (UITurnedOn)
        {
            ShowHealth();
            UpdateScore();
            UpdateTimer();
            ChangeWantedLevel();
        }

        if (PlayerController.Instance.GameOver)
        {
            GameOverUpdateScore();
        }
    }

    private void GameOverUpdateScore()
    {
        winScore.text  = "Score: " + PlayerController.Instance.score.ToString();
        loseScore.text = "Score: " + PlayerController.Instance.score.ToString();
    }

    void UpdateTimer()
    {
        time.text = Math.Round(PlayerController.Instance.timer, 0 ).ToString();
    }

    void UpdateScore()
    {
        score.text = PlayerController.Instance.score.ToString();
    }

    public void TurnUIOn(UIEnum panel)
    {
        String temp = panel.ToString();
        switch (temp)
        {
            case "UIPanel":
                UIPanel.SetActive(true);
                UITurnedOn = true;
                break;
            case "UIWinPanel":
                UIWinPanel.SetActive(true);
                break;
            case "UILosePanel":
                UILosePanel.SetActive(true);
                break;
            case "Canvas":
                Canvas.SetActive(true);
                break;
            default:
                Debug.Log("panel doesnt exist: " + temp);
                break;
        }
    }
    public void TurnUIOff(UIEnum panel)
    {
        String temp = panel.ToString();
        switch (temp)
        {
            case "UIPanel":
                UIPanel.SetActive(false);
                UITurnedOn = false;
                break;
            case "UIWinPanel":
                UIWinPanel.SetActive(false);
                break;
            case "UILosePanel":
                UILosePanel.SetActive(false);
                break;
            case "Canvas":
                Canvas.SetActive(false);
                break;
            default:
                Debug.Log("panel doesnt exist: " + temp);
                break;
        }
    }



    public void ShowHealth()
    {
        healthBar = PlayerController.Instance.currentHealth;
        healthIconHolder.GetComponent<Slider>().value = healthBar / 100;
    }

    public void ChangeWantedLevel()
    {
        wantedLevel = PlayerController.Instance.playerWantedLevel;
        switch (wantedLevel)
        {
            case 0:
                wantedIconHolder.GetComponent<Image>().sprite = wantedIcon[0];
                break;
            case 1:
                wantedIconHolder.GetComponent<Image>().sprite = wantedIcon[1];
                break;
            case 2:
                wantedIconHolder.GetComponent<Image>().sprite = wantedIcon[2];
                break;
            case 3:
                wantedIconHolder.GetComponent<Image>().sprite = wantedIcon[3];
                break;
            default:
                break;
        }
    }
    public void GetCurrentWeaponIcon()
    {
        icon = PlayerController.Instance.GetWeaponIcon();
        switch (icon)
        {
            case "Shotgun":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[0];
                break;
            case "SMG":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[1];
                break;
            case "Grunade":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[2];
                break;
            case "Kunai":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[3];
                break;
            case "Shurikan":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[4];
                break;
            case "RPG":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[5];
                break;
            case "Sword":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[6];
                break;
            case "Sniper":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[7];
                break;
            case "Rifle":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[8];
                break;
            case "Bat":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[9];
                break;
            case "Fists":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[10];
                break;
            case "Pistol":
                weaponIconHolder.GetComponent<Image>().sprite = weaponICons[11];
                break;

            default:
                Debug.Log("Not in icon list");
                break;
        }
        return;
    }


    public void Menu()
    {
        SpawningEnemies.Instance.ClearQueuess();
        SpawningNPCs.Instance.ClearQueuess();
        SceneChecker.Instance.TurnOFFNPC();

        PlayerController.Instance.GameOver = false;
        PlayerController.Instance.timer = 60;
        PlayerController.Instance.isDead = false;
        PlayerController.Instance.startTimer = false;

        Time.timeScale = 1;
        TurnUIOff(UIController.UIEnum.UIWinPanel);
        TurnUIOff(UIController.UIEnum.UILosePanel);


        Loader.Instance.Unload(Loader.Scene.City);
        Loader.Instance.Unload(Loader.Scene.Prison);
        Loader.Instance.Load(Loader.Scene.MainMenu);
    }
}
