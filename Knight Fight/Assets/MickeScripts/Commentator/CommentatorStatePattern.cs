using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CommentatorStatePattern : MonoBehaviour
{
    // **** STATE DECLARATIONS **** //
    private CommentatorAbstractClass currentState;

    private GameObject cameraObject;
    private GameObject playerObject;
    public GameObject commentatorImage;

    [HideInInspector] public CommentatorInactiveState inactiveState;
    [HideInInspector] public CommentatorIntroducingState introducingState;
    [HideInInspector] public CommentatorSilentState silentState;
    [HideInInspector] public CommentatorSpeakingState speakingState;

    [HideInInspector] public CameraStatePattern cameraScript;
    [HideInInspector] public PlayerStatePattern playerScript;

    [HideInInspector] public AudioCom audioCom;

    // **** COMMENTATOR GENERAL VARIABLES **** //
    [HideInInspector] public float postIntroTimer = 0.0f;
    [HideInInspector] public float hardSilenceTimer = 0.0f;
    [HideInInspector] public float randomCooldownTimer = 0.0f;
    [HideInInspector] public float boredCooldownTimer = 0.0f;

    [HideInInspector] public bool allowedToSpeak = false;

    // **** COMMENTATOR COOLDOWN VARIABLES **** //
    [Header("Speaking Frequency")]

    [HideInInspector] public bool introducingTrigger = true;
    [HideInInspector] public bool randomTrigger = false;
    [HideInInspector] public bool boredTrigger = false;
    [HideInInspector] public bool rareWeaponTrigger = false;
    [HideInInspector] public bool deathTrigger = false;
    [HideInInspector] public bool victoryTrigger = false;

    [Header("Guaranteed Silence Duration Between Comments")]
    public float hardSilenceDuration = 7.0f;

    [Header("Soft Silence Duration Between Comments")]
    public float silencePostIntro = 9.0f;
    public float randomSpeechFrequency = 25.0f;
    public float secondsUntilBored = 22.0f;


    void Awake()
    {
        introducingTrigger = true;

        inactiveState = new CommentatorInactiveState(this);
        introducingState = new CommentatorIntroducingState(this);
        silentState = new CommentatorSilentState(this);
        speakingState = new CommentatorSpeakingState(this);

        cameraScript = GetComponent<CameraStatePattern>();
        playerScript = GetComponent<PlayerStatePattern>();

        audioCom = GetComponent<AudioCom>();
    }

    void Start()
    {
        currentState = inactiveState;
    }

    void LateUpdate()
    {
        currentState.Execute();
    }

    public void ChangeState(CommentatorAbstractClass newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter();
    }
}