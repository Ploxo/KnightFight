using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquippedState : WeaponIState
{
    private readonly WeaponBaseClass weapon;

    public WeaponEquippedState(WeaponBaseClass weaponBase)
    {
        weapon = weaponBase;
    }

    public void OnStateEnter()
    {
        weapon.audioPlayer.WeaponBeingPickedUp();
        ChangePhysics();
        weapon.HeldPos();
        
    }
    public void UpdateState()
    {

    }

    public void ChangePhysics()
    {
        weapon.rb.isKinematic = true;
        weapon.col.enabled = false;
        weapon.rb.useGravity = false;
    }

    public void CollisionEnter(Collision col)
    {
        if (col.gameObject.tag == weapon.playerTag)
        {
            weapon.audioPlayer.AttackHittingPlayer();
        }
    }

    public void CollisionStay(Collision col)
    {
        
    }

}
