using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGroundedState : ProjectileIState
{
    private readonly ProjectileBase projectile;
    public ProjectileGroundedState(ProjectileBase projectileBase)
    {
        projectile = projectileBase;
    }


    public void ChangeState(ProjectileIState newState)
    {
        throw new System.NotImplementedException();
    }



    public void OnStateEnter()
    {
        projectile.damage = 0;
        projectile.gameObject.tag = "GroundedProjectile";
        Physics.IgnoreLayerCollision(projectile.gameObject.layer, projectile.player.layer, false);

    }

    public void UpdateState()
    {
        
    }

    public void CollisionStay(Collision col)
    {

    }

    public void CollisionEnter(Collision col)
    {
       
    }
}
