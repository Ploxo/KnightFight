using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System.Runtime.Serialization;

public class CrowdMoodSetter : MonoBehaviour
{
    public List<GameObject> crowdObjects;
    public GameObject menuObject;




    public void SetMood(int value)
    {
        if(value == 1)
        {
            menuObject.GetComponent<StudioEventEmitter>().EventInstance.setParameterByName("CrowdMood", 1);
        }

        else
        {
            menuObject.GetComponent<StudioEventEmitter>().EventInstance.setParameterByName("CrowdMood", 0);

        }






        //if(value == 1)
        //{
        //    menuObject.GetComponent<StudioEventEmitter>().EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //    foreach (GameObject crowd in crowdObjects)
        //    {
        //        crowd.GetComponent<StudioEventEmitter>().EventInstance.start();
        //    }
        //}
        //else if(value == 0)
        //{
        //    menuObject.GetComponent<StudioEventEmitter>().EventInstance.start();
        //    foreach (GameObject crowd in crowdObjects)
        //    {
        //        crowd.GetComponent<StudioEventEmitter>().EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //    }
        //}
    }
}
