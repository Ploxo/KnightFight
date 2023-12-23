using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerIState
{
    private readonly PlayerStatePattern player;
    private float internalStateTimer;

    public PlayerDashState(PlayerStatePattern statePatternPlayer)
    {
        player = statePatternPlayer;
    }

    public void OnStateEnter()
    {
        player.internalDashTimer = 0f;
        player.animator.SetBool("Dash", true);
        player.audioPlayer.PlayerDashing();

    }

    public void UpdateState()
    {
        internalStateTimer += Time.deltaTime;
        if (player.canDash)
        {
            if (internalStateTimer < player.dashDuration)
            {
                player.Dash();
            }
            else
            {
                player.RunOrIdleDecider();
            }
        }
        else
        {
            player.RunOrIdleDecider();
        }
    }
    public void ExitState()
    {
        player.animator.SetBool("Dash", false);
        player.internalGCDTimer = 0f;
        player.internalDashTimer = 0f;
        internalStateTimer = 0f;
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
