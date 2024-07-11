using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    public RandomSound[] policeSounds;
    public Sound[] music;

    [HideInInspector]public float masterSFX;
    [HideInInspector] public float masterMusic;

    void Awake()
    {
        
        foreach (Sound clip in sounds)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.name = clip.name;
            clip.source.clip = clip.sound;
        }
        foreach (RandomSound clip in policeSounds)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.clip = clip.sound;
        }
        foreach (Sound clip in music)
        {
            clip.source = gameObject.AddComponent<AudioSource>();
            clip.source.name = clip.name;
            clip.source.clip = clip.sound;
        }
    }

    void Update()
    {
        if (GameObject.Find("Options") != null)
        {
            masterSFX = VolueControl.Instance.Music.value;
            masterMusic = VolueControl.Instance.Music.value;
            foreach (Sound clip in sounds)
            {
                clip.source.volume = masterSFX;
            }
            foreach (RandomSound clip in policeSounds)
            {
                clip.source.volume = masterSFX;
            }
            foreach (Sound clip in music)
            {
                clip.source.volume = masterMusic;
            }

        }
    }



    public void PlaySoundLoop(string clipName)
    {
        Sound clip = Array.Find(music, sound => sound.name == clipName);
        if (clip == null)
        {
            return;
        }
        clip.source.loop = enabled;
        clip.source.Play();
    }
    public void StopSoundLoop(string clipName)
    {
        Sound clip = Array.Find(music, sound => sound.name == clipName);
        if (clip == null)
        {
            return;
        }
        clip.source.Stop();
    }


    public void PlaySound(string clipName)
    {
        Sound clip = Array.Find(sounds, sound => sound.name == clipName);
        if (clip == null)
        {
            return;
        }
        clip.source.Play();
    }
    public void PlayRandomPoliceSound()
    {
        int rand = UnityEngine.Random.Range(0, policeSounds.Length);
        RandomSound clip = policeSounds[rand];
        clip.source.Play();
    }

    public void SetSFXVolume(float volune)
    {
        masterSFX = volune;
    }
    public void SetMusicVolume(float volume )
    {
        masterMusic = volume;
    }
}
