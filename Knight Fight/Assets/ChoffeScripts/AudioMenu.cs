using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioMenu : MonoBehaviour
{
    [EventRef]
    public string buttonPress;
    public FMOD.Studio.EventInstance buttonPressed;

    [EventRef]
    public string startPress;
    public FMOD.Studio.EventInstance startPressed;

    [EventRef]
    public string playerJoin;
    public FMOD.Studio.EventInstance playerJoined;

    [EventRef]
    public string playerLeft;
    public FMOD.Studio.EventInstance playernotRight;

    [EventRef]
    public string menuMusic;
    public FMOD.Studio.EventInstance menuModeMusic;

    [EventRef]
    public string gameplayMusic;
    public FMOD.Studio.EventInstance gameplayModeMusic;



    public void ButtonPressed()
    {
        buttonPressed = RuntimeManager.CreateInstance(buttonPress);
        buttonPressed.start();
    }

    public void StartPressed()
    {
        startPressed = RuntimeManager.CreateInstance(startPress);
        startPressed.start();
    }

    public void PlayerJoined()
    {
        playerJoined = RuntimeManager.CreateInstance(playerJoin);
        playerJoined.start();
    }

    public void PlayerLeft()
    {
        playernotRight = RuntimeManager.CreateInstance(playerLeft);
        playernotRight.start();
    }

    public void StartMenuMusic()
    {
        menuModeMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        menuModeMusic = RuntimeManager.CreateInstance(menuMusic);
        menuModeMusic.start();
        gameplayModeMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StartGameplayMusic()
    {
        gameplayModeMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gameplayModeMusic = RuntimeManager.CreateInstance(gameplayMusic);
        gameplayModeMusic.start();
        menuModeMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
