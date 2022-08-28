using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeSettings : MonoBehaviour
{
    public TMP_InputField masterV_txtBx;
    public TMP_InputField sfxV_txtBx;
    public TMP_InputField musicV_txtBx;

    // Start is called before the first frame update
    void Start()
    {

        if(!PlayerPrefs.HasKey("master_volume"))
        {
            Debug.LogAssertion("No master Key");
            PlayerPrefs.SetFloat("master_volume", 1f);
        }
        float v = PlayerPrefs.GetFloat("master_volume");
        AudioManager.instance.ChangeMasterVolume(v);
        masterV_txtBx.SetTextWithoutNotify("" + v);

        if (!PlayerPrefs.HasKey("SFX_volume"))
        {
            Debug.LogAssertion("No SFX Key");
            PlayerPrefs.SetFloat("SFX_volume", 1f);
        }
        float v1 = PlayerPrefs.GetFloat("SFX_volume");
        AudioManager.instance.ChangeSFXVolume(v1);
        sfxV_txtBx.SetTextWithoutNotify("" + v1);

        if (!PlayerPrefs.HasKey("music_volume"))
        {
            Debug.LogAssertion("No music Key");
            PlayerPrefs.SetFloat("music_volume", 1f);
        }
        float v2 = PlayerPrefs.GetFloat("music_volume");
        AudioManager.instance.ChangeMusicVolume(v2);
        musicV_txtBx.SetTextWithoutNotify("" + v2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MasterVolume()
    {
        float value = float.Parse(masterV_txtBx.text);
        if (value > 1f || value < 0f)
        {
            masterV_txtBx.SetTextWithoutNotify(AudioManager.instance.masterVolume + "");
            return;
        }
        else
        {
            AudioManager.instance.ChangeMasterVolume(value);
            masterV_txtBx.SetTextWithoutNotify("" + value);

        }
    }
}
