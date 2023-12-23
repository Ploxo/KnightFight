using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinState : GameIState
{
    private float internalTimer = 0f;

    private readonly GameManager manager;

    public GameWinState(GameManager gameManager)
    {
        manager = gameManager;
    }

    public void OnStateEnter()
    {
        internalTimer = 0f;
        manager.cameraScript.focusedObject = manager.alivePlayers[0].transform;
        manager.cameraScript.ChangeState(manager.cameraScript.followPlayerState);
        manager.roundWinner.GetComponent<PlayerStatePattern>().StateChanger(manager.roundWinner.GetComponent<PlayerStatePattern>().winState);
        manager.winbanner.SetActive(true);
        foreach(GameObject player in manager.readyPlayers)
        {
            player.GetComponent<PlayerScoreTracker>().ClearScore();
        }
    }

    public void UpdateState()
    {
        if(internalTimer >= manager.winStateDuration)
        {
            manager.ToMenu();
        }
        internalTimer += Time.deltaTime;
    }
}
