using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(1f);
        Loader.Instance.Unload(Loader.Scene.LoadScene);
        Loader.Instance.Load(Loader.Scene.UI);
    }

}
