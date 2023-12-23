using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlyingState : ProjectileIState
{
    public int UnequippedLayer = 12;
    private readonly ProjectileBase projectile;
    private bool velocityApplied = false;
    private float internalGroundedTimer = 0f;
   


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
            //if (1 == (int)projectile.projectileType)
            //{
            //    projectile.rb.velocity = projectile.transform.forward * projectile.ProjectileSpeed;
            //}
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
        internalGroundedTimer = 0f;
        velocityApplied = false;
        LaunchFish();
        Physics.IgnoreLayerCollision(projectile.gameObject.layer, projectile.player.layer, true);
    }

    public void LaunchFish()
    {
        if (0 == (int)projectile.projectileType)
        {
            //forward
            if (0 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
            {
                projectile.rb.velocity = projectile.parentObject.transform.forward * projectile.ProjectileSpeed;
                velocityApplied = true;
            }
            //up
            else if (1 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
            {
                projectile.rb.velocity = projectile.parentObject.transform.up * projectile.ProjectileSpeed;
                velocityApplied = true;
            }
            //left
            else if (2 == (int)projectile.spellBook.GetComponent<WeaponBaseClass>().launchDir)
            {
                projectile.rb.velocity = projectile.parentObject.transform.right * -1 * projectile.ProjectileSpeed;
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
        else if (1 == (int)projectile.projectileType)
        {
            projectile.rb.velocity = projectile.parentObject.GetComponent<WeaponBananaTreePattern>().swordVel * projectile.ProjectileSpeed;
            velocityApplied = true;
        }
        else
        {

        }
    }

    public void CollisionStay(Collision col)
    {
        internalGroundedTimer += Time.deltaTime;
        if(internalGroundedTimer > 0.75)
        {
            ChangeState(projectile.groundedState);
        }
    }

    public void CollisionEnter(Collision col)
    {
        if(col.gameObject.tag == projectile.playerTag)
        {
            projectile.audioPlayer.HittingPlayer();
        }
    }
}
