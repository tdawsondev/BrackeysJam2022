using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SoundType 
{
    SFX,
    Music
}


[System.Serializable]
public class Sound
{
    public string name;

    public SoundType type;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;


    public AudioSource source;


}
