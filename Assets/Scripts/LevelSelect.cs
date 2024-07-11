using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadTutorial()
    {
        Loader.Instance.Load(Loader.Scene.Prison);
        Loader.Instance.Unload(Loader.Scene.MainMenu);


        //SceneManager.UnloadSceneAsync("MainMenu");
        //SceneManager.LoadScene("Prison", LoadSceneMode.Additive);
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
    public void LoadLevel1()
    {
        Loader.Instance.Unload(Loader.Scene.MainMenu);
        Loader.Instance.Load(Loader.Scene.City);

        //SceneManager.UnloadSceneAsync("MainMenu");
        //SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        //SceneManager.LoadScene("City", LoadSceneMode.Additive);
    }
}
