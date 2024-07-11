using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Loader : Singleton<Loader>
{
    public enum Scene
    {
        GameManager,
        UI,
        Prison,
        City,
        LoadScene,
        MainMenu
    }

    void Awake()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.LoadScene("GameManager", LoadSceneMode.Additive);
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }

    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameManager"));
    }

    public  void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
    }
    public  void Unload(Scene scene)
    {
        int temp = SceneManager.sceneCount;
        for (int i = 0; i < temp; i++)
        {
            if (SceneManager.GetSceneAt(i).name != scene.ToString())
            {
            }
            else
            {
                SceneManager.UnloadSceneAsync(scene.ToString());
            }
        }

    }
    
}
