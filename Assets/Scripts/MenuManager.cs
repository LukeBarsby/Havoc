using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuManager : Singleton<MonoBehaviour>
{
    public GameObject MenuCanvas;
    public GameObject LevelSelect;
    public GameObject CreditsCanvas;
    public GameObject SettingsCanvas;

    void Start()
    {
        AudioManager.Instance.PlaySoundLoop("MenuMusic");
        PlayerController.Instance.transform.position = Vector3.zero;
        PlayerController.Instance.DropWeapon();
    }
    void OnDestroy()
    {
        //AudioManager.Instance.StopSoundLoop("MenuMusic");
    }
    public void Play()
    {
        MenuCanvas.SetActive(false);
        LevelSelect.SetActive(true);
    }
    public void Settings()
    {
        MenuCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }
    public void Credits()
    {
        MenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SettingsBack()
    {
        SettingsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void CreditsBack()
    {
        CreditsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
    }
    public void LevelSelectBack()
    {
        LevelSelect.SetActive(false);
        MenuCanvas.SetActive(true);
    }



}
