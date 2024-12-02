using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    public AudioSource MusicAudioSource { get => musicAudioSource; }
    public AudioSource SfxAudioSource { get => sfxAudioSource; }

    private static SoundManager instance;

    private void Start() {
        ResetVolume();
        instance = this;
    }

    public static SoundManager Get() => instance;

    public void ResetVolume() {
        float musicVolume = 0.5f;
        float sfxVolume = 0.5f;
        if (PlayerPrefs.HasKey("Music Volume")) {
            musicVolume = PlayerPrefs.GetFloat("Music Volume");
        }
         if (PlayerPrefs.HasKey("SFX Volume")) {
            sfxVolume = PlayerPrefs.GetFloat("SFX Volume");
        }

        musicAudioSource.volume = musicVolume;
        sfxAudioSource.volume = sfxVolume; 
    }

    public void SaveVolume() {
        PlayerPrefs.SetFloat("Music Volume", musicAudioSource.volume);
        PlayerPrefs.SetFloat("SFX Volume", sfxAudioSource.volume);
    }
}
