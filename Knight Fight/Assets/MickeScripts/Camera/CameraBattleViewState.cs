using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBattleViewState : CameraAbstractClass
{
    // **** CONSTRUCTOR **** //
    public CameraBattleViewState(CameraStatePattern stateMachine)
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
        if (p_camera.objectsFollowedByCamera.Count == 0)
        {
            return;
        }

        p_camera.ViewChangeAcceleration();
        p_camera.SmoothnessNormalizer();

        ViewBattle();
        ZoomBehaviour();
    }

    public override void Exit()
    {
        //p_camera.cameraRestored = false;
    }

    private void ViewBattle()
    {
        Vector3 centerPoint = CalculateCenter();

        Vector3 finalPosition = centerPoint + p_camera.offsetFromObjects;

        p_camera.transform.position = Vector3.SmoothDamp(p_camera.transform.position, finalPosition, ref p_camera.velocity, p_camera.initialSmoothness);
        p_camera.transform.rotation = Quaternion.Lerp(p_camera.transform.rotation, p_camera.initialCameraRotation, Time.deltaTime);
    }

    private void ZoomBehaviour()
    {
        float battleFocus = Mathf.Lerp(p_camera.maxZoom, p_camera.minZoom, GreatestPlayerDistance() / p_camera.zoomLimiter);

        p_camera.gameCamera.fieldOfView = Mathf.Lerp(p_camera.gameCamera.fieldOfView, battleFocus, Time.deltaTime);
    }

    private Vector3 CalculateCenter()
    {
        var arenaBounds = new Bounds(p_camera.objectsFollowedByCamera[0].transform.position, Vector3.zero);

        for (int i = 0; i < p_camera.objectsFollowedByCamera.Count; i++)
        {
            arenaBounds.Encapsulate(p_camera.objectsFollowedByCamera[i].transform.position);
        }

        return arenaBounds.center;
    }

    private float GreatestPlayerDistance()
    {
        var arenaBounds = new Bounds(p_camera.objectsFollowedByCamera[0].transform.position, Vector3.zero);

        for (int i = 0; i < p_camera.objectsFollowedByCamera.Count; i++)
        {
            arenaBounds.Encapsulate(p_camera.objectsFollowedByCamera[i].transform.position);
        }

        return arenaBounds.size.x + arenaBounds.size.z;
    }
}