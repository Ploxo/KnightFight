using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class AudioProjectile : MonoBehaviour
{
    [EventRef]
    public string hitPlayer;
    public FMOD.Studio.EventInstance hittingPlayer;

    public void HittingPlayer()
    {
        RuntimeManager.PlayOneShot(hitPlayer, transform.position);
    }
}
