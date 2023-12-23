using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrownState : WeaponIState
{
    private readonly WeaponBaseClass weapon;
    private Vector3 throwVector;
    private bool movementApplied = false;
    private float internalGroundedTimer = 0f;

    public WeaponThrownState(WeaponBaseClass weaponBase)
    {
        weapon = weaponBase;
    }

    public void OnStateEnter()
    {
        internalGroundedTimer = 0f;
        Debug.Log("throwstate");
        if(weapon.thisWepType == WeaponBaseClass.Weapontype.throwable && weapon.attackActive == true)
        {
            if ((int)weapon.GetComponent<WeaponThrowable>().throwableType == 0)
            {
                movementApplied = true;
                Debug.Log("Attack with shield");
            }
            else
            {
                weapon.audioPlayer.WeaponBeingThrown();
                movementApplied = false;
                ChangePhysics();
                weapon.gameObject.tag = weapon.projectileTag;
                AddThrownForce();
                weapon.RemoveParentPlayer();
            }
            
        }
        else
        {
            weapon.audioPlayer.WeaponBeingThrown();
            movementApplied = false;
            ChangePhysics();
            weapon.gameObject.tag = weapon.projectileTag;
            AddThrownForce();
            weapon.RemoveParentPlayer();
            Debug.Log("throw");
        }
    }
    public void UpdateState()
    {
        if(weapon.rb.velocity == Vector3.zero)
        {
            weapon.ChangeState(weapon.unequippedState);
        }
    }
    public void ChangePhysics()
    {
        weapon.rb.isKinematic = false;
        weapon.col.enabled = true;
        weapon.rb.useGravity = true;
    }


    public void AddThrownForce()
    {
        //Throw the weapon the way the player is facing
        throwVector = new Vector3(weapon.parentPlayer.transform.forward.x, weapon.throwAngle, weapon.parentPlayer.transform.forward.z);
        weapon.rb.velocity = throwVector * weapon.thrownForce;
        movementApplied = true;
    }

    public void CollisionEnter(Collision col)
    {
        // här kan man lägga in att olika ljud ska spelas baserat på föremål man träffar.
        if (col.gameObject.tag == weapon.environmentTag)
        {
            if(weapon.thisWepType == WeaponBaseClass.Weapontype.throwable)
            {
                if ((int)weapon.GetComponent<WeaponThrowable>().throwableType == 0)
                {
                    weapon.GetComponent<audioThrowable>().Bounceing();
                }
            }

            else
            {
                weapon.audioPlayer.ThrownWepHittingEnvironment();
            } 
        }
        if (col.gameObject.tag == weapon.playerTag)
        {
            weapon.audioPlayer.AttackHittingPlayer();
        }
    }

    public void CollisionStay(Collision col)
    {
        internalGroundedTimer += Time.deltaTime;

        if (internalGroundedTimer > 0.75)
        {
            weapon.ChangeState(weapon.unequippedState);
        }
    }
}