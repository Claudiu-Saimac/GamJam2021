using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public AudioClip[] Clips;
    public Dictionary<string, AudioClip> AllSounds;

    // public enum Sounds
    // {
    //     GoodMilk,
    //     BadMilk,
    //     MilkClick,
    //     BowlClick,
    //     CerealClick
    // }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play(string clipName, bool loop)
    {
        AudioClip clip;
        AllSounds.TryGetValue(clipName, out clip);
        EffectsSource.clip = clip;
        EffectsSource.loop = loop;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(string clipName)
    {
        AudioClip clip;
        AllSounds.TryGetValue(clipName, out clip);
        MusicSource.clip = clip;
        MusicSource.Stop();
    }
}