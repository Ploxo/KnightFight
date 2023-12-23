using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArenaViewState : CameraAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CameraArenaViewState(CameraStatePattern stateMachine)
    {
        base.p_camera = stateMachine;
    }

    public override void Enter()
    {
        p_camera.initialSmoothness = 3.0f;
        p_camera.accelerationTimer = 0.0f;

        p_camera.initialCameraPosition = ViewEntireArena();
        p_camera.initialCameraRotation = p_camera.transform.rotation;
        p_camera.initialFieldOfView = p_camera.gameCamera.fieldOfView;

        p_camera.rotatingCounterClockwise = true;

        ViewEntireArena();
    }

    public override void Execute()
    {
        p_camera.ViewChangeAcceleration();

        RotateView();
    }

    public override void Exit()
    {
        p_camera.cameraRestored = false;
    }

    public Vector3 ViewEntireArena()
    {
        Vector3 position = new Vector3();

        position.x = p_camera.cameraPositionX;
        position.y = p_camera.cameraPositionY;
        position.z = p_camera.cameraPositionZ;

        p_camera.transform.position = Vector3.SmoothDamp(p_camera.transform.position, position, ref p_camera.velocity, p_camera.initialSmoothness);

        return position;
    }

    private void RotateView()
    {
        if (p_camera.rotatingCounterClockwise)
        {
            p_camera.transform.RotateAround(p_camera.centerPoint.transform.position, Vector3.up, p_camera.rotationSpeed * Time.deltaTime);
        }

        if (!p_camera.rotatingCounterClockwise)
        {
            p_camera.transform.RotateAround(p_camera.centerPoint.transform.position, -Vector3.up, p_camera.rotationSpeed * Time.deltaTime);
        }

        if (p_camera.transform.position.x < p_camera.maxRotationX)
        {
            p_camera.rotatingCounterClockwise = false;
        }

        if (p_camera.transform.position.x > p_camera.maxRotationY)
        {
            p_camera.rotatingCounterClockwise = true;
        }
    }
}