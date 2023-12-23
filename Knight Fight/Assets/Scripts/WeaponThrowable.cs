using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowable : WeaponBaseClass
{
    public float attackThrowForce;  
    public float durabilityDecrement;
    private float currentDurability;
    private float timeDelay = 0;
    private float timeDelayCol = 0;
    private bool playerHit = false;
    private bool thrown = false;
    private Vector3 constSpeed;
    public enum ThrowableType { throwableWeapon, trashWeapon};
    public ThrowableType throwableType;



    private void Awake()
     {
         unequippedState = new WeaponUnequippedState(this);
         equippedState = new WeaponEquippedState(this);
         thrownState = new WeaponThrownState(this);
         thisWepType = Weapontype.throwable;
         gameObject.GetComponent<Collider>().material.bounciness = 0;
         gameObject.GetComponent<Collider>().material.dynamicFriction = 0.6f;
         gameObject.GetComponent<Collider>().material.staticFriction = 0.6f;
         gameObject.GetComponent<Rigidbody>().angularDrag = 0.05f;
     }

     private void Start()
     {
         currentState = unequippedState;
         rb = GetComponent<Rigidbody>();
         col = GetComponent<Collider>();
         audioPlayer = GetComponent<AudioWeapon>();
     }

     private void Update()
     {
        currentState.UpdateState();
        if (attackActive == true)
        {
            timeDelayCol += Time.deltaTime;
            if (timeDelayCol >= 0.15)
            {
                Physics.IgnoreCollision(parentPlayer.GetComponent<Collider>(), col, false);
                timeDelayCol = 0;
                attackActive = false;
            }
            
        }
        // Kort delay så att spelaren hinner hämta skadan innan objektet förstörs
        if (playerHit == true)
        {
            timeDelay += Time.deltaTime;
            if (timeDelay >= 0.05)
            {
                ChangeDurability(durabilityDecrement);
                timeDelay = 0;
            }
        }
        if (thrown == true)
        {
            rb.velocity = rb.velocity.normalized * attackThrowForce;
        }
    }

     public override void Attack()
     {

     }

    public override void EndAttack()
    {
        attackActive = true;
        if (0 == (int)throwableType)
        {
            gameObject.layer = 14;
            col.material.bounciness = 1;
            col.material.dynamicFriction = 0;
            col.material.staticFriction = 0;
            transform.Rotate(90, 0, 0);
            rb.angularDrag = 0;
            gameObject.tag = "Throwable";
            rb.isKinematic = false;
            col.enabled = true;
            rb.useGravity = false;
            transform.rotation = Quaternion.Euler(90, 0, 0);
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            rb.velocity = parentPlayer.transform.forward * attackThrowForce;
            thrown = true;
        }

        RemoveParentPlayer();
        parentPlayer.GetComponent<PlayerStatePattern>().ThrowItem();
    }


    public override void ChangeDurability(float durabilityDecrement)
     {
        currentDurability -= durabilityDecrement;
        if (currentDurability <= 0)
        {
            //Rök effekt här
            GameObject smokeEffect = Instantiate(playSmokeEffect, transform.position, Quaternion.identity);
            Destroy(smokeEffect, 3);
            audioPlayer.WeaponBreaking();
            Destroy(this.gameObject);
        }
    }

     public override void ChangeState(WeaponIState newState)
     {
         currentState = newState;
         currentState.OnStateEnter();
     }

     public override void OnCollisionEnter(Collision collision)
     {
        currentState.CollisionEnter(collision);

        if (collision.gameObject.tag == playerTag && currentState == thrownState)
        {
            GameObject hitParticle = Instantiate(playClashEffect, transform.position, Quaternion.identity);
            Destroy(hitParticle, 3);
            //ChangeDurability(durabilityHitPlayer);
            playerHit = true;
        }
        
    }

    public override void OnCollisionStay(Collision collision)
    {
        if(currentState == thrownState && thrown == false)
        {
            currentState.CollisionStay(collision);
        }
        
    }

}
