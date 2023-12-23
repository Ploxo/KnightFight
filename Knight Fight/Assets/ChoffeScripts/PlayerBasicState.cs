using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicState : PlayerIState
{
    private readonly PlayerStatePattern player;

    public PlayerBasicState(PlayerStatePattern statePatternPlayer)
    {
        player = statePatternPlayer;
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("Running", true);
        
    }

    public void UpdateState()
    {
        player.ChangeDirection();
        player.Movement();
        if(player.moveDir == Vector2.zero)
        {
            player.StateChanger(player.idleState);
        }
    }
    public void ExitState()
    {
        player.animator.SetBool("Running", false);
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
