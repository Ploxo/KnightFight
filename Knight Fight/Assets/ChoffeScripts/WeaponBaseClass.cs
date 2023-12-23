using System.Collections;
using System.Collections.Generic;
using UnityEngine;
abstract public class WeaponBaseClass : MonoBehaviour
{
    public GameObject parentPlayer = null;
    public AudioWeapon audioPlayer;

    public WeaponIState currentState;
    [HideInInspector] public WeaponUnequippedState unequippedState;
    [HideInInspector] public WeaponEquippedState equippedState;
    [HideInInspector] public WeaponThrownState thrownState;
    public ParticleSystem[] attackVfx = null;

    public GameObject playClashEffect;
    public Transform clashEffectPosition;
    public GameObject playSmokeEffect;

    public enum Weapontype{ oneHSword, twoHSword, spellbook, throwable };
    public Weapontype thisWepType;
    public enum LaunchDir { forward, up, left, right };
    public LaunchDir launchDir;
    public float animationDuration = 1f;
    
    public float durability;
    public float damage;
    public float thrownDamage;
    public float thrownForce;
    public float throwAngle;
    public bool attackActive = false;

    public string environmentTag = "Environment";
    public string playerTag = "Player";
    public string projectileTag = "WeaponProjectile";
    public string weaponTag = "Weapon";

    public int UnequippedLayer = 12;
    public int EquippedLayer = 13;

    public Vector3 heldPosition;
    public Vector3 heldRotation;
    public Rigidbody rb;
    public Collider col;
    public WeaponSpawnManager weaponSpawnManager;

    public abstract void Attack();
    public abstract void EndAttack();
    
    public void ThrowWep()
    {
        ChangeState(thrownState);
    }
    public abstract void ChangeDurability(float durabilityDecrement);

    public void HeldPos()
    {
        transform.localPosition = heldPosition;
        transform.localEulerAngles = heldRotation;
    }

    public void OnDestroy()
    {
        GameObject spawnParticle = Instantiate(playSmokeEffect, transform.position, Quaternion.identity);
        Destroy(spawnParticle, 3);
        weaponSpawnManager = GameObject.FindObjectOfType<WeaponSpawnManager>();
        weaponSpawnManager.DestroySingleWeapon(this.gameObject);
    }


    //public void SetParentPlayer(Collision collision)
    //{


    //    parentPlayer = collision.gameObject;
    //    if(thisWepType == Weapontype.spellbook)
    //    {
    //        transform.SetParent(collision.gameObject.GetComponent<PlayerStatePattern>().leftHandGameobject.transform);
    //    }
    //    else
    //    {
    //        transform.SetParent(collision.gameObject.GetComponent<PlayerStatePattern>().rightHandGameobject.transform);
    //    }
    //}

    public void SetParentPlayer(GameObject collision)
    {


        parentPlayer = collision.gameObject;
        if (thisWepType == Weapontype.spellbook)
        {
            transform.SetParent(collision.gameObject.GetComponent<PlayerStatePattern>().leftHandGameobject.transform);
        }
        else
        {
            transform.SetParent(collision.gameObject.GetComponent<PlayerStatePattern>().rightHandGameobject.transform);
        }
    }

    public void RemoveParentPlayer()
    {
        transform.parent = null;
    }

    public abstract void OnCollisionEnter(Collision collision);
    public abstract void OnCollisionStay(Collision collision);
    public abstract void ChangeState(WeaponIState newState);
}
