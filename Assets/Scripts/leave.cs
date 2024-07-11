using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Loader.Instance.Unload(Loader.Scene.Prison);
            Loader.Instance.Load(Loader.Scene.MainMenu);
        }
    }


}
