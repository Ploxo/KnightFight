using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentatorSilentState : CommentatorAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CommentatorSilentState(CommentatorStatePattern stateMachine)
    {
        base.p_commentator = stateMachine;
    }

    public override void Enter()
    {
        p_commentator.allowedToSpeak = false;
    }

    public override void Execute()
    {
        if (!p_commentator.allowedToSpeak)
        {
            Debug.Log("Silent");

            p_commentator.randomCooldownTimer += Time.deltaTime;
            p_commentator.boredCooldownTimer += Time.deltaTime;

            if (p_commentator.hardSilenceTimer < p_commentator.hardSilenceDuration)
            {
                p_commentator.hardSilenceTimer += Time.deltaTime;
            }
        }

        if (p_commentator.randomCooldownTimer > p_commentator.randomSpeechFrequency)
        {
            p_commentator.randomTrigger = true;
            p_commentator.ChangeState(p_commentator.speakingState);
        }

        if (p_commentator.boredCooldownTimer > p_commentator.secondsUntilBored)
        {
            p_commentator.boredTrigger = true;
            p_commentator.ChangeState(p_commentator.speakingState);
        }

        if (p_commentator.hardSilenceTimer >= p_commentator.hardSilenceDuration && p_commentator.deathTrigger)
        {
            p_commentator.boredTrigger = false;
            p_commentator.ChangeState(p_commentator.speakingState);
        }

        if (p_commentator.victoryTrigger)
        {
            p_commentator.boredTrigger = false;
            p_commentator.ChangeState(p_commentator.speakingState);
        }
    }

    public override void Exit()
    {
        p_commentator.hardSilenceTimer = 0.0f;
        p_commentator.randomCooldownTimer = 0.0f;
        p_commentator.boredCooldownTimer = 0.0f;
    }
}