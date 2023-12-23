using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrowFishPattern : WeaponBaseClass
{
    public float durabilityDecrement;
    public GameObject weaponAmmo;
    public int numberofAmmoToSpawn;
    public float spawnOfsettDist;
    public float vShape;
    
    private float currentDurability;
    private float spawnOffset = 0;
    private int increaseSpawnOffsett = 1;
    


    private void Awake()
    {
        unequippedState = new WeaponUnequippedState(this);
        equippedState = new WeaponEquippedState(this);
        thrownState = new WeaponThrownState(this);
        thisWepType = Weapontype.spellbook;
        
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
    }

    public override void Attack()
    {

    }
    public override void EndAttack()
    {
        //launch projectile and instanciate projectile 
        // Projectile i eget script med en OnCollisonEnter kollar om träffat en spelare och isfall gå in i enemy.gameObject.GetComponent<PlayerStatePattern>().OnHit(damage);
        for (int i = 0; i < numberofAmmoToSpawn; i++)
        {
            audioPlayer.Attacking();
            Vector3 spawnOffsetVec = new Vector3(0, 0, 0);
            GameObject temp = Instantiate(weaponAmmo, parentPlayer.GetComponent<PlayerStatePattern>().projectileSpawnPos.transform.position, Quaternion.identity);
            spawnOffsetVec = temp.transform.localPosition - parentPlayer.transform.position;
            Vector3 spawnOffsetVecStore = spawnOffsetVec;
            float spawnOffsetVecNorm = Mathf.Sqrt(spawnOffsetVec.x * spawnOffsetVec.x + spawnOffsetVec.z * spawnOffsetVec.z);
            spawnOffsetVec.x = spawnOffsetVec.x - spawnOffset * spawnOffsetVecStore.z / spawnOffsetVecNorm;
            spawnOffsetVec.z = spawnOffsetVec.z + spawnOffset * spawnOffsetVecStore.x / spawnOffsetVecNorm;
            temp.transform.localPosition = spawnOffsetVec + parentPlayer.transform.position - (spawnOffsetVecStore * i / vShape);
            temp.GetComponent<ProjectileFish>().parentObject = parentPlayer.GetComponent<PlayerStatePattern>().projectileSpawnPos;
            temp.GetComponent<ProjectileFish>().spellBook = this.gameObject;
            temp.GetComponent<ProjectileFish>().player = parentPlayer;
            spawnOffset = spawnOffset * (-1);
            Debug.Log(parentPlayer.GetComponent<PlayerStatePattern>().projectileSpawnPos.transform.forward);
            increaseSpawnOffsett++;
            if (increaseSpawnOffsett == 2)
            {

                spawnOffset += spawnOfsettDist;
                increaseSpawnOffsett = 0;
            }

            //transform.DetachChildren();
        }
        spawnOffset = 0;
        ChangeDurability(durabilityDecrement);
    }

    public override void ChangeDurability(float durabilityDecrement)
    {
        currentDurability -= durabilityDecrement;
        if (currentDurability <= 0)
        {
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
    }

    public override void OnCollisionStay(Collision collision)
    {
        currentState.CollisionStay(collision);
    }
}
