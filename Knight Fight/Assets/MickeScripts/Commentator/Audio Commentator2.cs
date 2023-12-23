using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioCommentator2 : MonoBehaviour
{
    [EventRef]
    public string onVictory;
    public FMOD.Studio.EventInstance onWin;

    [EventRef]
    public string bored;
    public FMOD.Studio.EventInstance boredDialogue;

    [EventRef]
    public string intro;
    public FMOD.Studio.EventInstance introduction;

    public void InterruptSpeech()
    { 
        onWin.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        boredDialogue.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        introduction.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    public void OnWin()
    {
        InterruptSpeech();
        introduction = RuntimeManager.CreateInstance(intro);
        introduction.start();
    }

    public void Bored()
    {
        InterruptSpeech();
        introduction = RuntimeManager.CreateInstance(intro);
        introduction.start();
    }

    public void Intro()
    {
        InterruptSpeech();
        introduction = RuntimeManager.CreateInstance(intro);
        introduction.start();
    }
}
