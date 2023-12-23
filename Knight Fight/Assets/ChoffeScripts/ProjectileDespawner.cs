using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDespawner : MonoBehaviour
{

    public void DestroyObjectsWithTag(string objectTag)
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag(objectTag);
        foreach(GameObject obj in temp)
        {
            Destroy(obj);
        }
    }
}
