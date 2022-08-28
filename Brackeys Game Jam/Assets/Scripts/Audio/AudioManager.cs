using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }
    [Range(0f, 1f)]
    public float masterVolume;
    [Range(0f, 1f)]
    public float sfxVolume;
    [Range(0f, 1f)]
    public float musicVolume;
    public Sound[] sounds;


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        s.source.volume = s.volume;
        if (s.type == SoundType.SFX)
        {
            s.source.volume = s.source.volume * sfxVolume;
        }
        if (s.type == SoundType.Music)
        {
            s.source.volume = s.source.volume * musicVolume;
        }
        s.source.volume = s.source.volume * masterVolume;
        s.source.Play();


    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        if (s.source.isPlaying)
        {
            s.source.Stop();
        }
    }

    public bool SongIsPlaying(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return false;
        }
        return s.source.isPlaying;
    }

    public void PlayAtPositition(string name, Transform t, bool isPlayer)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        s.source.volume = s.volume;
        if (s.type == SoundType.SFX)
        {
            s.source.volume = s.source.volume * sfxVolume;
        }
        if (s.type == SoundType.Music)
        {
            s.source.volume = s.source.volume * musicVolume;
        }
        s.source.volume = s.source.volume * masterVolume;



        // Debug.Log("Playing: " + name);
        AudioSource.PlayClipAtPoint(s.source.clip, t.position, s.source.volume);

    }
    public void PlayAtPositition(string name, Transform t)
    {
        PlayAtPositition(name, t, false);
    }

    public void ChangeMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("master_volume", value);
        masterVolume = value;
        AdjustCurrentSounds();
    }
    public void ChangeSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFX_volume", value);
        sfxVolume = value;
        AdjustCurrentSounds();
    }
    public void ChangeMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("music_volume", value);
        musicVolume = value;
        AdjustCurrentSounds();


    }
    public void AdjustCurrentSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            if (s.type == SoundType.SFX)
            {
                s.source.volume = s.source.volume * sfxVolume;
            }
            if (s.type == SoundType.Music)
            {
                //Debug.LogWarning(s.source.volume);
                s.source.volume = s.source.volume * musicVolume;
            }
            s.source.volume = s.source.volume * masterVolume;
        }
    }


    private void Start()
    {
        //Debug.LogWarning("Why");
        //Play("Background");
    }
    private void Update()
    {

    }

}
