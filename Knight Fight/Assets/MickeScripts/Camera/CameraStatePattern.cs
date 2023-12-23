using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraStatePattern : MonoBehaviour
{
    // **** STATE DECLARATIONS **** //
    private CameraAbstractClass currentState;

    [HideInInspector] public CameraArenaViewState arenaViewState;
    [HideInInspector] public CameraBattleViewState battleViewState;
    [HideInInspector] public CameraFollowPlayerState followPlayerState;

    [HideInInspector] public PlayerStatePattern playerObject;

    // **** CAMERA GENERAL VARIABLES **** //
    [HideInInspector] public Camera gameCamera;

    [HideInInspector] public Vector3 velocity = Vector3.zero;   // Referenced to in SmoothDamp functions

    [HideInInspector] public float initialSmoothness = 3.0f;
    [HideInInspector] public float accelerationTimer = 0.0f;

    [HideInInspector] public Vector3 initialCameraPosition;
    [HideInInspector] public Quaternion initialCameraRotation;
    [HideInInspector] public float initialFieldOfView;

    [HideInInspector] public bool cameraRestored;
    [HideInInspector] public bool gameFinished;

    private float restoreTimer;

    [Header("Camera Movement")]

    [Range(0.0f, 0.99f)]
    public float ViewChangeSpeed = 0.25f;      // Higher value = slower zoom

    [Range(0.0f, 0.99f)]
    public float desiredSmoothness = 0.2f;      // Higher value = smoother camera

    // **** ARENA VIEW VARIABLES **** //
    [Header("Menu View")]

    public float cameraPositionX =  -5.0f;      // These are only suggested values
    public float cameraPositionY =  75.0f;      // Change as desired in Inspector
    public float cameraPositionZ = -30.0f;      // Goal is to give a full arena view

    public GameObject centerPoint;

    public float maxRotationX = -10.25f;
    public float maxRotationY = 6.25f;

    public float rotationSpeed = 2.5f;

    [HideInInspector] public bool rotatingCounterClockwise;

    // **** BATTLE VIEW VARIABLES **** //
    [Header("Battle View")]

    public List<GameObject> objectsFollowedByCamera;

    public Vector3 offsetFromObjects = new Vector3(0.0f, 30.0f, -17.5f);

    public float minZoom = 40.0f;
    public float maxZoom = 10.0f;

    [Range(0, 100)]
    public float zoomLimiter = 50.0f;

    // **** FOLLOW PLAYER VARIABLES **** //     // NOT TO BE USED IN THE ACTUAL GAME CURRENTLY!!! Testing purposes only
    [Header("Follow Specific Object")]

    public Transform focusedObject;

    public float offsetFromFocusY = 30.0f;       // By changing the distance from the player on the Y and Z axises,
    public float offsetFromFocusZ = 10.0f;       // we avoid the camera planting itself straight inside the player

    void Awake()
    {
        gameCamera = GetComponent<Camera>();

        arenaViewState = new CameraArenaViewState(this);
        battleViewState = new CameraBattleViewState(this);
        followPlayerState = new CameraFollowPlayerState(this);

        cameraRestored = true;
        gameFinished = false;
}

    void Start()
    {
        currentState = arenaViewState;
        currentState.Enter();
    }

    void LateUpdate()
    {
        if (cameraRestored && !gameFinished)
        {
            currentState.Execute();
        }

        if (!cameraRestored)
        {
            ViewChangeAcceleration();
            SmoothnessNormalizer();

            SmoothRestoreCamera();
        }

        if (gameFinished)
        {
            InstantRestoreCamera();
        }
    }

    public void ChangeState(CameraAbstractClass newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter();
    }

    public void ViewChangeAcceleration()
    {
        if (initialSmoothness > desiredSmoothness)
        {
            for (float i = initialSmoothness; i > desiredSmoothness; i = i - 0.0005f)
            {
                accelerationTimer += Time.deltaTime;

                if (accelerationTimer > ViewChangeSpeed)
                {
                    initialSmoothness = initialSmoothness - 0.0005f;

                    accelerationTimer = 0;
                }
            }
        }
    }

    public void SmoothnessNormalizer()
    {
        if (initialSmoothness < desiredSmoothness)
        {
            initialSmoothness = desiredSmoothness;
        }
    }

    public void SmoothRestoreCamera()
    {
        restoreTimer += Time.deltaTime;
        gameCamera.fieldOfView = 60.0f;

        transform.position = Vector3.SmoothDamp(transform.position, initialCameraPosition, ref velocity, initialSmoothness);
        transform.rotation = Quaternion.Lerp(transform.rotation, initialCameraRotation, Time.deltaTime);

        if (restoreTimer > 1.75f)
        {
            restoreTimer = 0.0f;
            cameraRestored = true;
        }
    }

    public void InstantRestoreCamera()
    {
        transform.position = initialCameraPosition;
        transform.rotation = initialCameraRotation;
        gameCamera.fieldOfView = 60.0f;

        gameFinished = false;
        cameraRestored = true;
    }
}