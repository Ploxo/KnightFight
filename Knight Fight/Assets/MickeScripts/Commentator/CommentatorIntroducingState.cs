using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentatorIntroducingState : CommentatorAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CommentatorIntroducingState(CommentatorStatePattern stateMachine)
    {
        base.p_commentator = stateMachine;
    }

    public override void Enter()
    {
        p_commentator.allowedToSpeak = true;
        p_commentator.commentatorImage.SetActive(true);
    }

    public override void Execute()
    {
        if (p_commentator.allowedToSpeak && p_commentator.introducingTrigger)
        {
            Debug.Log("HELLOOOO EVERYONE!!!"); //REPLACE THIS LINE WITH ACTUAL AUDIO FUNCTION! 

            p_commentator.audioCom.Intro();

            p_commentator.introducingTrigger = false;
            p_commentator.allowedToSpeak = false;
        }

        p_commentator.postIntroTimer += Time.deltaTime;

        if (p_commentator.postIntroTimer > p_commentator.silencePostIntro)
        {
            p_commentator.ChangeState(p_commentator.silentState);
        }

        if (p_commentator.deathTrigger)
        {
            p_commentator.ChangeState(p_commentator.speakingState);
        }

        if (p_commentator.victoryTrigger)
        {
            p_commentator.ChangeState(p_commentator.speakingState);
        }
    }

    public override void Exit()
    {
        p_commentator.hardSilenceTimer = p_commentator.postIntroTimer;
        p_commentator.boredCooldownTimer = 0.0f;
        p_commentator.commentatorImage.SetActive(false);
    }
}