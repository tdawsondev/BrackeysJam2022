using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerUIAssistant : MonoBehaviour
{
    AudioManager am;


    // Start is called before the first frame update
    void Start()
    {
        am = AudioManager.instance;
    }

    public void Play(string name)
    {
        am.Play(name);
    }
}
