using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip sound;
    [HideInInspector]public AudioSource source;
    [Range(0, 1)]
    public float volume;
}

