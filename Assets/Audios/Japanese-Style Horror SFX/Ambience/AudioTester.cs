using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioTester : MonoBehaviour
{
    public static AudioTester instancia;

    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("UI")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Clipes")]
    public AudioClip backgroundMusic;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("Volume atual: " + musicSlider.value);
        musicSource.Play();
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSFXVolume;

        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Clamp(musicSlider.value, 0.0001f, 1f);
        mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MusicVolumeKey, volume);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Clamp(sfxSlider.value, 0.0001f, 1f);
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFXVolumeKey, volume);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

}
