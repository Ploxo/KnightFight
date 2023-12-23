using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnequippedState : WeaponIState
{
    private readonly WeaponBaseClass weapon;

    public WeaponUnequippedState(WeaponBaseClass weaponBase)
    {
        weapon = weaponBase;
    }

    public void OnStateEnter()
    {
        weapon.gameObject.layer = weapon.UnequippedLayer;
        ChangePhysics();
        if (weapon.parentPlayer != null)
        {
            Physics.IgnoreCollision(weapon.parentPlayer.GetComponent<Collider>(), weapon.col, false);
            weapon.parentPlayer = null;
        }
        weapon.gameObject.tag = weapon.weaponTag;

    }
    public void UpdateState()
    {

    }

    public void ChangePhysics()
    {
        weapon.rb.isKinematic = false;
        weapon.col.enabled = true;
        weapon.rb.useGravity = true;
    }

    public void CollisionEnter(Collision col)
    {
        //spelaren hanterar all pickup
    }

    public void CollisionStay(Collision col)
    {
        
    }

}
