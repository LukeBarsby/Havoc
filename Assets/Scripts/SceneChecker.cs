using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChecker : Singleton<SceneChecker>
{
    [SerializeField] GameObject GameManager;

    void Awake()
    {
        GameManager.SetActive(false);
    }
    public void TurnOnNPCs()
    {
        GameManager.SetActive(true);
    }
    public void TurnOFFNPC()
    {
        GameManager.SetActive(false);
    }
}
