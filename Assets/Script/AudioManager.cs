using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class BaseSound
    {
        public string name;
        public AudioClip clip;
    }

    private static AudioManager _instance;
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public List<BaseSound> AllSounds;

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

    public void Play(string clipName, bool loop = false)
    {
        AudioClip clip = null;
        foreach (var baseSound in AllSounds.Where(sunet => sunet.name == clipName))
        {
            clip = baseSound.clip;
        }

        EffectsSource.clip = clip;
        EffectsSource.loop = loop;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void Stop(string clipName)
    {
        AudioClip clip = null;
        foreach (var baseSound in AllSounds.Where(sunet => sunet.name == clipName))
        {
            clip = baseSound.clip;
        }

        EffectsSource.clip = clip;
        EffectsSource.Stop();
    }


    public void PlayMusic(string clipName, bool loop = false)
    {
        AudioClip clip = null;
        foreach (var baseSound in AllSounds.Where(sunet => sunet.name == clipName))
        {
            clip = baseSound.clip;
        }

        MusicSource.clip = clip;
        MusicSource.loop = loop;
        MusicSource.Play();
    }

    // Play a single clip through the music source.
    public void StopMusic(string clipName)
    {
        AudioClip clip = null;
        foreach (var baseSound in AllSounds.Where(sunet => sunet.name == clipName))
        {
            clip = baseSound.clip;
        }

        MusicSource.clip = clip;
        MusicSource.Stop();
    }
}