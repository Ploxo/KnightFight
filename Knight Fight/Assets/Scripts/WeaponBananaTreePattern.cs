using FMOD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBananaTreePattern : WeaponBaseClass
{
    public float durabilityDecrement;
    public GameObject weaponAmmo;
    public GameObject projectileSpawnPoint;
    public float timeDelayProjectile;
    public float upSpeedProjectile;
    [HideInInspector]public Vector3 swordVel;

    private float currentDurability;
    private bool newAttack = false;
    private float timeDelayTimer = 0;

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
    }

    private void Update()
    {
        currentState.UpdateState();
        timeDelayTimer += Time.deltaTime;
        if (attackActive && timeDelayTimer >= timeDelayProjectile)
        { 
            GameObject temp = Instantiate(weaponAmmo, projectileSpawnPoint.transform.position, Quaternion.identity);
            temp.GetComponent<ProjectileBanana>().parentObject = this.gameObject;

            temp.GetComponent<ProjectileBanana>().player = parentPlayer;

            temp.GetComponent<ProjectileBanana>().spellBook = this.gameObject;
            timeDelayTimer = 0f;
            Vector3 swordDir = gameObject.transform.position - parentPlayer.transform.position;
            Vector3 swardDir = swordDir;
            swardDir.y = 0;
            swordVel = new Vector3(-swardDir.z,upSpeedProjectile,swardDir.x); 
            
        }
    }

    public override void Attack()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        audioPlayer.Attacking();
        newAttack = true;
        attackActive = true;
        timeDelayTimer = -0.5f;
    }
    public override void EndAttack()
    {
        col.enabled = false;
        attackActive = false;

    }

    public override void ChangeDurability(float durabilityDecrement)
    {
        currentDurability -= durabilityDecrement;
        if (currentDurability <= 0)
        {
            GameObject smokeParticle = Instantiate(playSmokeEffect, transform.position, Quaternion.identity);
            Destroy(smokeParticle, 3);
            audioPlayer.WeaponBreaking();
            parentPlayer.GetComponent<PlayerStatePattern>().weaponDestroyed = true;
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
            ChangeDurability(durabilityDecrement);
            newAttack = false;
        }

    }

    public override void OnCollisionStay(Collision collision)
    {
        currentState.CollisionStay(collision);
    }

}
