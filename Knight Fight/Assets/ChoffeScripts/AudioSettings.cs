using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    public Slider masterVolumeSlider;
    public float masterVolume = 1f;
    public TextMeshProUGUI masterVolumeText;

    public Slider sfxVolumeSlider;
    public FMOD.Studio.VCA sfx;
    public float sfxVolume = 50f;
    public TextMeshProUGUI sfxVolumeText;

    public Slider musicVolumeSlider;
    public FMOD.Studio.VCA music;
    public float musicVolume = 50f;
    public TextMeshProUGUI musicVolumeText;

    public Slider commentatorVolumeSlider;
    public FMOD.Studio.VCA commentator;
    public float commentatorVolume = 75f;
    public TextMeshProUGUI commentatorVolumeText;

    public Slider crowdVolumeSlider;
    public FMOD.Studio.VCA crowd;
    public float crowdVolume = 50f;
    public TextMeshProUGUI crowdVolumeText;

    [Range(0.001f,1f)] public float test = 0.5f;

    private void Awake()
    {
        sfx = RuntimeManager.GetVCA("VCA:/Sfx");
        music = RuntimeManager.GetVCA("VCA:/Music");
        commentator = RuntimeManager.GetVCA("VCA:/Commentators");
        crowd = RuntimeManager.GetVCA("VCA:/Crowd");

        ChangeSFX(sfxVolume);
        ChangeMusic(musicVolume);
        ChangeCommentator(commentatorVolume);
        ChangeCrowd(crowdVolume);
    }
    public void Update()
    {
        sfx.setVolume(sfxVolume * masterVolume);
        music.setVolume(musicVolume * masterVolume);
        commentator.setVolume(commentatorVolume * masterVolume);
        crowd.setVolume(crowdVolume * masterVolume);
    }

    public void ChangeMaster(float change)
    {
        masterVolumeSlider.value = change;
        masterVolume = change / 100;
        SetPercentage(masterVolumeSlider.value, masterVolumeText);
    }

    public void ChangeSFX(float change)
    {
        sfxVolumeSlider.value = change;
        sfxVolume = change / 100;
        SetPercentage(sfxVolumeSlider.value, sfxVolumeText);
    }

    public void ChangeMusic(float change)
    {
        musicVolumeSlider.value = change;
        musicVolume = change / 100;
        SetPercentage(musicVolumeSlider.value, musicVolumeText);
    }

    public void ChangeCommentator(float change)
    {
        commentatorVolumeSlider.value = change;
        commentatorVolume = change / 100;
        SetPercentage(commentatorVolumeSlider.value, commentatorVolumeText);
    }

    public void ChangeCrowd(float change)
    {
        crowdVolumeSlider.value = change;

        crowdVolume = change / 100;
        SetPercentage(crowdVolumeSlider.value, crowdVolumeText);
    }

    public void SetPercentage(float value, TextMeshProUGUI text)
    {
        
        text.text = Mathf.RoundToInt(value) + "%";
    }
}
