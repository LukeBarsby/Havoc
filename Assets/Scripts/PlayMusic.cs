using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string Play;
    void Start()
    {
        AudioManager.Instance.PlaySoundLoop(Play);
    }
    void OnDestroy()
    {
        AudioManager.Instance.StopSoundLoop(Play);
    }


}
