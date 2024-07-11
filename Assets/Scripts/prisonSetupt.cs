using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prisonSetupt : MonoBehaviour
{
   

    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Prison"));
        PlayerController.Instance.currentHealth = 100f;
    }

}
