using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class AudioPlayer : MonoBehaviour
{
    public int givenGender;

    public void OnEnable()
    {
        givenGender = Random.Range(0, 2);
    }

    [EventRef]
    public string playerHurtMale;
    public FMOD.Studio.EventInstance playerHurtingMale;

    [EventRef]
    public string playerHurtFemale;
    public FMOD.Studio.EventInstance playerHurtingFemale;

    [EventRef]
    public string playerDash;
    public FMOD.Studio.EventInstance playerDashing;

    [EventRef]
    public string playerThrow;
    public FMOD.Studio.EventInstance playerThrowing;

    [EventRef]
    public string playerPunch;
    public FMOD.Studio.EventInstance playerPunching;



    public void PlayerThrowing()
    {
        RuntimeManager.PlayOneShot(playerThrow, transform.position);
    }

    public void PlayerDashing()
    {
        RuntimeManager.PlayOneShot(playerDash, transform.position);
    }

    public void PlayerPunching()
    {
        RuntimeManager.PlayOneShot(playerPunch, transform.position);
    }

    public void PlayerHurting()
    {
        if(givenGender == 0)
        {
            PlayerHurtingFemale();
        }
        else
        {
            PlayerHurtingMale();
        }

    }
    private void PlayerHurtingMale() //Spelar alla vapenljud för male
    {
        RuntimeManager.PlayOneShot(playerHurtMale, transform.position);
    }
    private void PlayerHurtingFemale() //Spelar alla vapenljud för female
    {
        Debug.Log("femalePlayer got hurt");
        RuntimeManager.PlayOneShot(playerHurtFemale, transform.position);
    }
}
