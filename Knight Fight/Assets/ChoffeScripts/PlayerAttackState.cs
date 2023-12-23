using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerIState
{
    private readonly PlayerStatePattern player;
    public float internalStateTimer = 0f;

    public PlayerAttackState(PlayerStatePattern statePatternPlayer)
    {
        player = statePatternPlayer;
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("Attack", true);
        player.Attack();
        internalStateTimer = 0f;
    }

    public void UpdateState()
    {
        player.ChangeDirection();
        if(player.weapon != null)
        {
            if (internalStateTimer >= player.attackAnimDuration)
            {
            
                player.RunOrIdleDecider();
            }
        }
        else if(internalStateTimer >= player.fistAnimDuration)
        {
            player.RunOrIdleDecider();
        }
        internalStateTimer += Time.deltaTime;
    }

    public void ExitState()
    {
        if(player.weapon != null)
        {
            player.weapon.GetComponent<WeaponBaseClass>().EndAttack();
        }

        player.animator.SetBool("Attack", false);
        player.leftFist.SetActive(false);
        internalStateTimer = 0f;
        player.internalGCDTimer = 0f;
        player.internalAttackTimer = 0f;
    }

    public void TakeDamage(float damage)
    {
        player.health -= damage;
        if (player.health <= 0)
        {
            player.Die();
        }
    }
}
