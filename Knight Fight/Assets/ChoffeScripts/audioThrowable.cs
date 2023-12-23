using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class audioThrowable : MonoBehaviour
{
    [EventRef]
    public string ThrowableBounceEnviroment;
    public FMOD.Studio.EventInstance bounceing;



    public void Bounceing()
    {
        RuntimeManager.PlayOneShot(ThrowableBounceEnviroment, transform.position);
    }
}