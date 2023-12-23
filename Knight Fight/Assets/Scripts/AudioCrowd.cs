using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioCrowd : MonoBehaviour
{
    [EventRef]
    public string crowdCheer;
    public FMOD.Studio.EventInstance cheering;

    public void Cheer()
    {
        RuntimeManager.PlayOneShot(crowdCheer, transform.position);
    }

}
