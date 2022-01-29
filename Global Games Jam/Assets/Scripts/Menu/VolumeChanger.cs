using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    public AudioMixer audioMixer;
 
    public void SetVolumeMusic(float vol)
    {
        audioMixer.SetFloat("MusicVolume", vol);
    }

    public void SetVolumeSFX(float vol)
    {
        audioMixer.SetFloat("SFXVolume", vol);
    }
}
