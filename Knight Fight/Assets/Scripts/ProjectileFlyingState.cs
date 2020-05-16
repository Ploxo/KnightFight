﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlyingState : ProjectileIState
{
    private readonly ProjectileBase projectile;
    private bool velocityApplied = false;
   


    public ProjectileFlyingState(ProjectileBase projectileBase)
    {
        projectile = projectileBase;
    }

    public void UpdateState()
    {
        if (velocityApplied)
        {
           if(projectile.rb.velocity == Vector3.zero)
            {
                    ChangeState(projectile.groundedState);
            }
        }

    }

    public void ChangeState(ProjectileIState newState)
    {
        if (newState == projectile.groundedState)
        {
            velocityApplied = false;
            projectile.StateChanger(projectile.groundedState);
        }
    }


    public void OnStateEnter()
    {
        velocityApplied = false;
        LaunchFish();
        Debug.Log("FlyingState");
    }

    public void LaunchFish()
    {
        //forward
        if (0 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
        {
            projectile.rb.velocity = projectile.parentObject.transform.forward * projectile.ProjectileSpeed;
            velocityApplied = true;
        }
        //up
        else if(1 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
        {
            projectile.rb.velocity = projectile.parentObject.transform.up * projectile.ProjectileSpeed;
            velocityApplied = true;
        }
        //left
        else if (2 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
        {
            projectile.rb.velocity = projectile.parentObject.transform.right*-1 * projectile.ProjectileSpeed;
            velocityApplied = true;
        }
        //right
        else if (3 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
        {
            projectile.rb.velocity = projectile.parentObject.transform.right * projectile.ProjectileSpeed;
            velocityApplied = true;
        }
        else
        {

        }


    }


}