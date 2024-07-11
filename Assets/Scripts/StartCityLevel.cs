using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCityLevel : MonoBehaviour
{
    
    void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("City"));
        PlayerController.Instance.currentHealth = 100f;
        SceneChecker.Instance.TurnOnNPCs();
        GameManager.Instance.Start();
        SpawningNPCs.Instance.StartGame();
        SpawningEnemies.Instance.Start();
    }
}
