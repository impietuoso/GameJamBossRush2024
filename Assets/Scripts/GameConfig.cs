using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConfig : MonoBehaviour {
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown windowModeDropdown;

    public Slider bgmSlider;
    public Slider sfxSlider;

    public Toggle bgmToggle;
    public Toggle sfxToggle;

    public AudioSource bgmAudio;
    public AudioSource sfxAudio;

    int resolutionValue;
    int windowModeValue;

    bool canChangeValues;

    Resolution[] resolutions;
    List<Resolution> filteredResoltions;

    double currentRefreshRate;
    int currentResolutionIndex;

    private void Start() {
        bgmAudio.volume = PlayerPrefs.GetFloat("BGM", 0.3f);
        bgmSlider.value = bgmAudio.volume;
        if (bgmAudio.volume == 0) bgmAudio.mute = false;

        sfxAudio.volume = PlayerPrefs.GetFloat("SFX", 0.2f);
        sfxSlider.value = sfxAudio.volume;
        if (sfxAudio.volume == 0) sfxAudio.mute = false;

        SetResolutions();

        canChangeValues = true;
    }

    private void Update() {
        bgmAudio.mute = !bgmToggle.isOn;
        sfxAudio.mute = !sfxToggle.isOn;

        if (canChangeValues) {
            if (bgmAudio != null) {
                bgmAudio.volume = bgmSlider.value;
                bgmSlider.value = bgmAudio.volume;
                if (bgmAudio.volume == 0) bgmAudio.mute = true;
                else bgmAudio.mute = false;
                PlayerPrefs.SetFloat("BGM", bgmAudio.volume);
            }

            if (sfxAudio != null) {
                sfxAudio.volume = sfxSlider.value;
                sfxSlider.value = sfxAudio.volume;
                if (sfxAudio.volume == 0) sfxAudio.mute = true;
                else sfxAudio.mute = false;
                PlayerPrefs.SetFloat("SFX", sfxAudio.volume);
            }
        }
    }

    public void SetResolutions() {
        resolutions = Screen.resolutions;
        filteredResoltions = new List<Resolution>();
        resolutionDropdown.ClearOptions();

        currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        for (int i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].refreshRateRatio.value == currentRefreshRate) {
                filteredResoltions.Add(resolutions[i]);
            }
        }

        List<string> options = new();
        for (int i = 0; i < filteredResoltions.Count; i++) {
            string resolutionOption = filteredResoltions[i].width + "x" + filteredResoltions[i].height + " " + filteredResoltions[i].refreshRateRatio.value + " Hz";
            options.Add(resolutionOption);
            if (filteredResoltions[i].width == Screen.width && filteredResoltions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void BGMMute() { bgmAudio.mute = bgmToggle.isOn; }

    public void SFXMute() { sfxAudio.mute = sfxToggle.isOn; }

    public void UpdateResolution() {
        resolutionValue = resolutionDropdown.value;
        Resolution resolution = filteredResoltions[resolutionValue];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void UpdateWindowMode() {
        windowModeValue = windowModeDropdown.value;
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void PauseGame() {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
    }

    public void LoadScene(int scene) {
        SceneManager.LoadSceneAsync(scene);
    }

    public void CloseGame() {
        Application.Quit();
    }

}