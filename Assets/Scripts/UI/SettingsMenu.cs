using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class SettingsMenu : MonoBehaviour {
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void OnEnable() {
        fullscreenToggle.isOn = Screen.fullScreen;
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(
            Screen.resolutions
                .Select((res) => res.width + "x" + res.height + " " + res.refreshRateRatio + "hz")
                .ToList<string>()
        );

        musicVolumeSlider.value = AudioManager.Instance.SoundtrackVolume;
        sfxVolumeSlider.value = AudioManager.Instance.SFXVolume;
        resolutionDropdown.Select();
    }

    public void ClickApplySettings() {
        var selectedResolution = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);
        AudioManager.Instance.SaveVolume();
    }

    public void ClickResetSettings() {
        AudioManager.Instance.ResetVolume();
    }

    public void OnMusicVolumeChanged(float volume) {
        AudioManager.Instance.SoundtrackVolume = volume;
    }

    public void OnSFXVolumeChanged(float volume) {
        AudioManager.Instance.SFXVolume = volume;
    }
}
