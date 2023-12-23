using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollHandler : MonoBehaviour
{
    public List<Rigidbody> rigidbodies;
    public Transform animRig;
    public Transform ragdollRig;

    public void Awake()
    {
        foreach(Rigidbody index in ragdollRig.GetComponentsInChildren<Rigidbody>())
        {
            rigidbodies.Add(index);
        }
    }

    public void SetRagdollActive()
    {
        ragdollRig.gameObject.SetActive(true);
        ResetRagdollTransforms(animRig, ragdollRig);
        
        foreach(Rigidbody index in ragdollRig.GetComponentsInChildren<Rigidbody>())
        {
            index.isKinematic = false;
            index.useGravity = true;
            
            if (index.GetComponent<Collider>())
            {
                index.GetComponent<Collider>().enabled = true;
            }        
        }
        animRig.gameObject.SetActive(false);
    }
    public void SetRagdollInactive()
    {
        animRig.gameObject.SetActive(true);
        ResetRagdollTransforms(animRig, ragdollRig);
        
        foreach (Rigidbody index in ragdollRig.GetComponentsInChildren<Rigidbody>())
        {
            index.isKinematic = true;
            index.useGravity = false;
            if (index.GetComponent<Collider>())
            {
                index.GetComponent<Collider>().enabled = false;
            }
        }
        ragdollRig.gameObject.SetActive(false);
    }

    private void ResetRagdollTransforms(Transform sourceTransform, Transform destinationTransform)
    {
        for(int i = 0; i < sourceTransform.transform.childCount; i++)
        {
            Transform source = sourceTransform.GetChild(i);
            Transform destination = destinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            ResetRagdollTransforms(source, destination);
        }
    }


}
