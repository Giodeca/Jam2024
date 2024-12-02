using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioClip soundtrackToPlayFromStart;

    [Header("Sounds")]
    [SerializeField] List<Sound> soundtrackList = new List<Sound>();
    [SerializeField] List<Sound> SFXList = new List<Sound>();
    [SerializeField] List<Sound> UI_SFXList = new List<Sound>();
    [SerializeField] List<Sound> longSFXList = new List<Sound>();

    [Header("Audio Sources")]
    [SerializeField] public AudioSource soundtrackSource;
    [SerializeField] public AudioSource SFXSource;

    public float SoundtrackVolume { get => soundtrackSource.volume; set => soundtrackSource.volume = value; }
    public float SFXVolume { get => SFXSource.volume; set => SFXSource.volume = value; }

    private void Start()
    {
        ResetVolume();
        if(soundtrackToPlayFromStart != null)
        {
            soundtrackSource.clip = soundtrackToPlayFromStart;
            soundtrackSource.Play();
        }
    }

    public void ResetVolume() {
        float musicVolume = 0.5f;
        float sfxVolume = 1f;
        if (PlayerPrefs.HasKey("Music Volume")) {
            musicVolume = PlayerPrefs.GetFloat("Music Volume");
        }
         if (PlayerPrefs.HasKey("SFX Volume")) {
            sfxVolume = PlayerPrefs.GetFloat("SFX Volume");
        }

        soundtrackSource.volume = musicVolume;
        SFXSource.volume = sfxVolume; 
    }

    public void SaveVolume() {
        PlayerPrefs.SetFloat("Music Volume", soundtrackSource.volume);
        PlayerPrefs.SetFloat("SFX Volume", SFXSource.volume);
    }

    #region Soundtrack
    /// <summary>
    /// Play a specific soundtrack.
    /// </summary>
    /// <param name="tag">Name of the soundtrack.</param>
    public void PlaySoundtrack(string tag)
    {
        AudioClip clip = SearchClip(soundtrackList, tag);

        if(clip == null)
        {
            Debug.LogWarning("Sound was not found inside the list.");
        }
        else
        {
            soundtrackSource.clip = clip;
            soundtrackSource.Play();
        }
    }

    /// <summary>
    /// Play a specific soundtrack and set the volume.
    /// </summary>
    /// <param name="tag">Name of the soundtrack.</param>
    /// <param name="volume">Value of the volume. Goes from 0 to 1.</param>
    public void PlaySoundtrack(string tag, float volume)
    {
        AudioClip clip = SearchClip(soundtrackList, tag);

        if (clip == null)
        {
            Debug.LogWarning("Soundtrack was not found inside the list.");
        }
        else
        {
            soundtrackSource.volume = volume;
            soundtrackSource.clip = clip;
            soundtrackSource.Play();
        }
    }

    /// <summary>
    /// Stop the current soundtrack being played.
    /// </summary>
    public void StopSoundtrack()
    {
        soundtrackSource.Stop();
        soundtrackSource.clip = null;
    }

    /// <summary>
    /// Mute the soundtrack Audio Source.
    /// </summary>
    /// <param name="muted">State of the mute value.</param>
    public void MuteSoundtrackSource(bool muted)
    {
        soundtrackSource.mute = muted;
    }
    #endregion

    #region SFX
    /// <summary>
    /// Play a specific SFX.
    /// </summary>
    /// <param name="tag">Name of the SFX.</param>
    /// <param name="volume">Value of the volume. Goes from 0 to 1.</param>
    public void PlaySFX(string tag, float volume = 1)
    {
        float internalVolume;
        if(volume != 1)
        {
            internalVolume = volume;
        }
        else
        {
            internalVolume = SFXSource.volume;
        }
        AudioClip clip = SearchClip(SFXList, tag);
        print(clip.name);
        if (clip == null)
        {
            Debug.LogWarning(clip.name + "SFX was not found inside the list.");
        }
        else
        {
            SFXSource.PlayOneShot(clip, internalVolume);
        }
    }
    #endregion

    #region Long SFX
    /// <summary>
    /// Play a specific long SFX.
    /// </summary>
    /// <param name="tag">Name of the long SFX.</param>
    public void PlayLongSFX(string tag)
    {
        AudioClip clip = SearchClip(longSFXList, tag);

        if (clip == null)
        {
            Debug.LogWarning("Sound was not found inside the list.");
        }
        else
        {
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }

    /// <summary>
    /// Play a specific long SFX and set the volume.
    /// </summary>
    /// <param name="tag">Name of the long SFX.</param>
    /// <param name="volume">Value of the volume. Goes from 0 to 1.</param>
    public void PlayLongSFX(string tag, float volume)
    {
        AudioClip clip = SearchClip(longSFXList, tag);

        if (clip == null)
        {
            Debug.LogWarning("Soundtrack was not found inside the list.");
        }
        else
        {
            SFXSource.volume = volume;
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }

    /// <summary>
    /// Play a specific 3D long SFX at a positionand set the volume.
    /// </summary>
    /// <param name="tag">Name of the long SFX.</param>
    /// <param name="position">Position to play the 3D sound at.</param>
    /// <param name="volume">Value of the volume. Goes from 0 to 1.</param>
    public void Play3DLongSFX(string tag, Vector3 position, float volume = 1f)
    {
        AudioClip clip = SearchClip(soundtrackList, tag);

        if (clip == null)
        {
            Debug.LogWarning("Soundtrack was not found inside the list.");
        }
        else
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }

    /// <summary>
    /// Stop the current soundtrack being played.
    /// </summary>
    public void StopLongSFX()
    {
        SFXSource.Stop();
        SFXSource.clip = null;
    }

    /// <summary>
    /// Mute the SFX Audio Source.
    /// </summary>
    /// <param name="muted">State of the mute value.</param>
    public void MuteSFXSource(bool muted)
    {
        SFXSource.mute = muted;
    }
    #endregion

    #region UI SFX
    /// <summary>
    /// Play a specific UI SFX.
    /// </summary>
    /// <param name="tag">Name of the SFX.</param>
    /// <param name="volume">Value of the volume. Goes from 0 to 1.</param>
    public void PlayUISFX(string tag, float volume = 1f)
    {
        AudioClip clip = SearchClip(UI_SFXList, tag);

        if (clip == null)
        {
            Debug.LogWarning("SFX was not found inside the list.");
        }
        else
        {
            SFXSource.PlayOneShot(clip, volume);
        }
    }
    #endregion

    // Search a clip by its tag inside a Sound list
    private AudioClip SearchClip(List<Sound> list, string tag)
    {
        AudioClip clip = null;

        foreach (Sound sound in list)
        {
            if(sound.tag == tag)
            {
                clip = sound.clip;
                break;
            }
        }

        return clip;
    }
}

[Serializable]
class Sound
{
    public string tag;
    public AudioClip clip;
}
