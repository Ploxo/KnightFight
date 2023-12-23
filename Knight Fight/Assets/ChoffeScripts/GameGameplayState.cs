using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameGameplayState : GameIState
{
    private bool lowHPPlayer = false;
    private CountdownTimer countdown;
    private readonly GameManager manager;

    public GameGameplayState(GameManager gameManager)
    {
        manager = gameManager;
        countdown = GameObject.Find("CountdownTimer").GetComponent<CountdownTimer>();
        countdown.gameObject.SetActive(false);
    }
    public void OnStateEnter()
    {
        //disable players here and enable again after countdown == easy fix for many rounds
        manager.projectileDespawner.DestroyObjectsWithTag(manager.groundedProjectileTag);
        manager.projectileDespawner.DestroyObjectsWithTag(manager.projectileTag);
        manager.counterManager.countdownIsDone = false;

        manager.ResetMusicParams();
        manager.DisablePlayers();
        manager.AddPlayersForCamera();
        manager.crowdMoodSetter.SetMood(1);
        manager.cameraScript.ChangeState(manager.cameraScript.battleViewState);
        manager.audioManager.StartGameplayMusic();
        manager.commentatorScript.ChangeState((manager.commentatorScript.introducingState));

        manager.weaponSpawnManager.DestroyWeapons();
        countdown.ResetTimer();
        manager.internalRoundDelayTimer = 0f;
        countdown.counting = true;
        manager.menuCanvas.gameObject.SetActive(false);
        manager.newRoundProcessStarted = false;
      
    }

    public void UpdateState()
    {
        manager.TriggerMusicCheckpoints(manager.GetGlobalHealthPercentage());
        manager.SetLowHealthMusic();

        if (manager.counterManager.countdownIsDone == true)
        {
            manager.EnablePlayers();
            //manager.inputManagerScript.trigger = true;
            manager.inputManagerScript.SpawnPlayers();
            //manager.counterManager.countdownIsDone = false;

        }
        manager.weaponSpawnManager.TimerUpdater();

        if(manager.newRoundProcessStarted == false)
        {
            manager.CheckForRoundWinner();
        }
        if (manager.newRoundProcessStarted)
        {
            manager.internalRoundDelayTimer += Time.deltaTime;
        }
        if (manager.internalRoundDelayTimer >= manager.newRoundDelayDuration)
        {
            manager.CheckForWinner();
        }
    }
}
