using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwordPattern : WeaponBaseClass
{
    public float durabilityDecrement;
    private float currentDurability;
    private bool newAttack = false;
    
    private void Awake()
    {
        unequippedState = new WeaponUnequippedState(this);
        equippedState = new WeaponEquippedState(this);
        thrownState = new WeaponThrownState(this);
    }

    private void Start()
    {
        
        currentState = unequippedState;
        currentDurability = durability;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        audioPlayer = GetComponent<AudioWeapon>();
        foreach(ParticleSystem particle in attackVfx)
        {
            particle.Stop();
        }
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public override void Attack()
    {
        Debug.Log("Enters attack");
        col.enabled = true;
        audioPlayer.Attacking();
        if(attackVfx != null)
        {
            foreach (ParticleSystem particle in attackVfx)
            {
                particle.Play();
            }
        }
        
        newAttack = true;
    }

    public override void EndAttack()
    {
        Debug.Log("Exits attack");
        col.enabled = false;
        if (attackVfx != null)
        {
            foreach (ParticleSystem particle in attackVfx)
            {
                particle.Stop();
            }
        }
    }

    public override void ChangeDurability(float durabilityDecrement)
    {
        currentDurability -= durabilityDecrement;
        if (currentDurability <= 0)
        {
            parentPlayer.GetComponent<PlayerStatePattern>().weaponDestroyed = true;
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
        if (collision.gameObject.tag == playerTag && newAttack == true)
        {
            GameObject playClashParticle = Instantiate(playClashEffect, clashEffectPosition.position, clashEffectPosition.rotation);
            Destroy(playClashParticle, 3);
            ChangeDurability(durabilityDecrement);
            newAttack = false;
        }        
    }

    public override void OnCollisionStay(Collision collision)
    {
        currentState.CollisionStay(collision);
    }
}
