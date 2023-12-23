using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowState : PlayerIState
{
    private readonly PlayerStatePattern player;
    public float internalStateTimer = 0f;

    public PlayerThrowState(PlayerStatePattern statePatternPlayer)
    {
        player = statePatternPlayer;
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("Throw", true);
        player.audioPlayer.PlayerThrowing();
    }

    public void UpdateState()
    {
        player.ChangeDirection();
        if(internalStateTimer >= player.throwAnimDuration)
        {
            player.ThrowItem();
            player.RunOrIdleDecider();
        }
        else
        {
            internalStateTimer += Time.deltaTime;
        }
    }
    public void ExitState()
    {
        player.animator.SetBool("Throw", false);
        internalStateTimer = 0f;
        player.internalGCDTimer = 0f;
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
