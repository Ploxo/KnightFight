using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerState : CameraAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CameraFollowPlayerState(CameraStatePattern stateMachine)
    {
        base.p_camera = stateMachine;
    }

    public override void Enter()
    {
        p_camera.initialSmoothness = 3.0f;
        p_camera.accelerationTimer = 0.0f;
    }

    public override void Execute()
    {
        p_camera.ViewChangeAcceleration();
        p_camera.SmoothnessNormalizer();

        FollowPlayer();
    }

    public override void Exit()
    {
        p_camera.gameFinished = true;
    }

    private void FollowPlayer()
    {
        Vector3 position = new Vector3();

        position.x = p_camera.focusedObject.position.x;
        position.y = p_camera.focusedObject.position.y + p_camera.offsetFromFocusY;
        position.z = p_camera.focusedObject.position.z - p_camera.offsetFromFocusZ;

        p_camera.transform.position = Vector3.SmoothDamp(p_camera.transform.position, position, ref p_camera.velocity, p_camera.initialSmoothness);
    }
}