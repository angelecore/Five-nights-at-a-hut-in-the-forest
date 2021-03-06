using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private string masterVolumeParam = "MasterVolume";

    [SerializeField]
    private string musicVolumeParam = "MusicVolume";

    [SerializeField]
    private string effectsVolumeParam = "EffectsVolume";

    private const string MusicVolumeKey = "MusicVolume";
    private const string EffectsVolumeKey = "EffectsVolume";

    private const float MinVolume = 0.0001f;

    public float MusicVolume => PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
    public float EffectsVolume => PlayerPrefs.GetFloat(EffectsVolumeKey, 1f);
    
    void Start()
    {
        SetMusicVolume(MusicVolume);
        SetEffectsVolume(EffectsVolume);
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume(masterVolumeParam, volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetVolume(musicVolumeParam, volume);
        SaveMusicVolume(volume);
    }

    public void SetEffectsVolume(float volume)
    {
        SetVolume(effectsVolumeParam, volume);
        SaveEffectsVolume(volume);
    }

    private void SetVolume(string parameter, float volume)
    {
        var mixerVolume = volume <= MinVolume ? -80 : Mathf.Log(volume) * 20;
        mixer.SetFloat(parameter, mixerVolume);
    }

    private static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }
    private static void SaveEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat(EffectsVolumeKey, volume);
    }
}
