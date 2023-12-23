using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentatorInactiveState : CommentatorAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CommentatorInactiveState(CommentatorStatePattern stateMachine)
    {
        base.p_commentator = stateMachine;
    }

    public override void Enter()
    {
        //Nope();
    }

    public override void Execute()
    {
        //DoAbsolutelyJackAll();
    }

    public override void Exit()
    {
        //Leggo();
    }
}