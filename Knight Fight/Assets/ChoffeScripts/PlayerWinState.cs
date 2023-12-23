using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinState : PlayerIState
{
    private readonly PlayerStatePattern player;

    public PlayerWinState(PlayerStatePattern statePatternPlayer)
    {
        player = statePatternPlayer;
    }

    public void OnStateEnter()
    {
        player.animator.SetBool("Win", true);
    }

    public void ExitState()
    {
        player.animator.SetBool("Win", false);
    }

    public void TakeDamage(float damage)
    {
        //borde inte vara möjligt
    }

    public void UpdateState()
    {
        if (player.gameManager.gameState != player.gameManager.winState)
        {
            player.RunOrIdleDecider();
        }
    }
}
