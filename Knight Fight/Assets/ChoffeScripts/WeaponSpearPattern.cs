using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpearPattern : WeaponBaseClass
{
    public float attackZone = 1.0f;
    public float durabilityDecrement;
    public Vector3 offestAttackZonePos;
    // Start is called before the first frame update
    private float currentDurability;
    Weapontype thisWeaponType;

    private void Awake()
    {
        unequippedState = new WeaponUnequippedState(this);
        equippedState = new WeaponEquippedState(this);
        thrownState = new WeaponThrownState(this);
        thisWeaponType = Weapontype.twoHSword;
    }
    

    private void Start()
    {
        currentState = unequippedState;
        currentDurability = durability;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public override void Attack()
    {
        
        //Vector3 attackZoneCenter = this.gameObject.transform.TransformPoint(offestAttackZonePos);
        //Collider[] hitPlayer = Physics.OverlapSphere(attackZoneCenter, attackZone);
        //foreach (Collider enemy in hitPlayer)
        //{
            
            //// delar endast ut dmg på spelaren och inte andra objekt som råkar bli träffad och inte på sig själv
            //if (enemy.gameObject.layer == 8)
            //{
            //    if (enemy.gameObject.name == parentPlayer.gameObject.name)
            //    {
            //      //Gör inget 
            //      //Debug.Log(this.gameObject.GetComponentInParent<GameObject>().name + " Hit my self " + enemy.gameObject.name); 
                 
            //    }
            //    else
            //    {
            //      ChangeDurability(durabilityDecrement);
            //      enemy.gameObject.GetComponent<PlayerStatePattern>().OnHit(damage);
            //      Debug.Log("Attack");

            //    }
            //}
        //}
       
    }
    public override void EndAttack()
    {
        col.enabled = false;
    }

    public override void ChangeDurability(float durabilityDecrement)
    {
        currentDurability -= durabilityDecrement;
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
