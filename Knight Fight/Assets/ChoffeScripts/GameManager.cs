using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.InputSystem.InputAction;

public class GameManager : MonoBehaviour
{
    //statemachine objects
    public GameIState gameState;
    public GameGameplayState gameplayState;
    public GameMenuState menuState;
    public GameWinState winState;

    //lists used by the gamemanager
    //private List<Gamepad> inputDevices;

    public List<InputDevice> inputDevices;

    public List<GameObject> readyPlayers;
    public List<GameObject> alivePlayers;

    //components and scripts
    public GameObject cameraObject;
    public Canvas menuCanvas;
    public GameObject gameMenu;
    public GameObject commentatorImage;
    [HideInInspector] public CameraStatePattern cameraScript;
    [HideInInspector] public PlayerInputManager inputManagerScript;
    [HideInInspector] public CommentatorStatePattern commentatorScript;
    public CrowdMoodSetter crowdMoodSetter;
    public AudioMenu audioManager;
    public WeaponSpawnManager weaponSpawnManager;
    public CounterManager counterManager;
    public ProjectileDespawner projectileDespawner;
    public GameObject winbanner;

    //rounds
    public int amountOfRounds = 1;
    public TextMeshProUGUI roundsText;
    [HideInInspector] public GameObject roundWinner;
    [HideInInspector] public bool newRoundProcessStarted = false;

    //durations
    public float winStateDuration = 5f;
    public float newRoundDelayDuration = 2f;
    public float internalRoundDelayTimer = 0f;

    public float combinedStartingHealth = 0;
    public float combinedCurrentHealth = 0;

    [Header("Music Triggers in % of total player Health")]
    public float firstHitTriggerValue = 1f;
    public float halfHealthTriggerValue = 0.5f;
    public float lowHealthTriggerValue = 25f;

    public bool lowHPPlayer = false;

    //player related components
    public GameObject player1;
    public GameObject player1Ready;
    public GameObject player1NotReady;

    public GameObject player2;
    public GameObject player2Ready;
    public GameObject player2NotReady;

    public GameObject player3;
    public GameObject player3Ready;
    public GameObject player3NotReady;

    public GameObject player4;
    public GameObject player4Ready;
    public GameObject player4NotReady;

    //Tags
    public string groundedProjectileTag = "GroundedProjectile";
    public string projectileTag = "Projectile";
    public string inputHandlerTag = "InputHandler";

    public void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        amountOfRounds = 1;
        gameplayState = new GameGameplayState(this);
        menuState = new GameMenuState(this);
        winState = new GameWinState(this);

        cameraScript = cameraObject.GetComponent<CameraStatePattern>();
        inputManagerScript = GameObject.FindObjectOfType<PlayerInputManager>();
        audioManager = GetComponent<AudioMenu>();
        commentatorScript = cameraObject.GetComponent<CommentatorStatePattern>();
        weaponSpawnManager = GetComponent<WeaponSpawnManager>();
        counterManager = GetComponent<CounterManager>();
        projectileDespawner = GetComponent<ProjectileDespawner>();

        //inputDevices = new List<Gamepad>();

        inputDevices = new List<InputDevice>();

        readyPlayers = new List<GameObject>();
        gameState = menuState;
        AwakeSetup();

    }
    public void AwakeSetup()
    {
        audioManager.StartMenuMusic();
        crowdMoodSetter.SetMood(0);
        AddInputDevices();
    }

    public void Update()
    {
        gameState.UpdateState();

    }

    public void OnStart()
    {
        if(readyPlayers.Count >= 2)
        {
            alivePlayers.Clear();
            combinedStartingHealth = 0f;
            foreach(GameObject player in readyPlayers)
            {
                alivePlayers.Add(player);
                combinedStartingHealth += player.GetComponent<PlayerStatePattern>().maxHealth;
            }
            combinedCurrentHealth = combinedStartingHealth;
            audioManager.StartPressed();
            gameState = gameplayState;
            gameState.OnStateEnter();
        }
    }

    public void OnExit()
    {
        audioManager.ButtonPressed();
        Application.Quit();
    }

    public void AddInputDevices()
    {
        foreach (Gamepad index in Gamepad.all)
        {
            inputDevices.Add(index);
        }
        //foreach(InputDevice index in InputDevice.all)
        //{
        //    inputDevices.Add(index);
        //}
    }

    public void OnJoin(CallbackContext context)
    {
        if (gameState == menuState && gameMenu.active)
        {
            if (context.performed)
            {
            if (!inputDevices.Contains(context.control.device))
            {
                inputDevices.Add(context.control.device);
                Debug.Log("added " + context.control.device);
            }
            if(context.control.device == inputDevices[0])
            {
                if (!readyPlayers.Contains(player1))
                {
                    audioManager.PlayerJoined();
                    Debug.Log("beep");
                    player1NotReady.SetActive(false);
                    player1Ready.SetActive(true);
                    inputManagerScript.player1 = true;
                    readyPlayers.Add(player1);
                }
                else
                {
                    audioManager.PlayerLeft();
                    Debug.Log("bop");
                    player1NotReady.SetActive(true);
                    player1Ready.SetActive(false);
                    inputManagerScript.player1 = false;
                    readyPlayers.Remove(player1);
                }
            }
            else if (context.control.device == inputDevices[1])
            {
                if (!readyPlayers.Contains(player2))
                {
                    audioManager.PlayerJoined();
                    
                    player2NotReady.SetActive(false);
                    player2Ready.SetActive(true);
                    inputManagerScript.player2 = true;
                    readyPlayers.Add(player2);
                }
                else
                {
                    audioManager.PlayerLeft();
                    
                    player2NotReady.SetActive(true);
                    player2Ready.SetActive(false);
                    inputManagerScript.player2 = false;
                    readyPlayers.Remove(player2);
                }

            }
            else if (context.control.device == inputDevices[2])
            {
                if (!readyPlayers.Contains(player3))
                {
                    audioManager.PlayerJoined();
                    player3NotReady.SetActive(false);
                    player3Ready.SetActive(true);
                    inputManagerScript.player3 = true;
                    readyPlayers.Add(player3);
                }
                else
                {
                    audioManager.PlayerLeft();
                    player3NotReady.SetActive(true);
                    player3Ready.SetActive(false);
                    inputManagerScript.player3 = false;
                    readyPlayers.Remove(player3);
                }

            }
            else if (context.control.device == inputDevices[3])
            {
                if (!readyPlayers.Contains(player4))
                {
                    audioManager.PlayerJoined();
                    player4NotReady.SetActive(false);
                    player4Ready.SetActive(true);
                    inputManagerScript.player4 = true;
                    readyPlayers.Add(player4);
                }
                else
                {
                    audioManager.PlayerLeft();
                    player4NotReady.SetActive(true);
                    player4Ready.SetActive(false);
                    inputManagerScript.player4 = false;
                    readyPlayers.Remove(player4);
                }

            }
            }
            
        }
 
    }

    public void AddPlayersForCamera()
    {
        cameraScript.objectsFollowedByCamera.Clear();
        foreach (GameObject player in readyPlayers)
        {
            cameraScript.objectsFollowedByCamera.Add(player);
        }
    }

    public void RemovePlayersForCamera()
    {
        readyPlayers.Clear();
        alivePlayers.Clear();
        cameraScript.objectsFollowedByCamera.Clear();
    }
    public void EnablePlayers()
    {
        foreach(GameObject player in readyPlayers)
        {
            player.SetActive(true);
        }
    }
    public void DisablePlayers()
    {
        foreach (GameObject player in readyPlayers)
        {
            player.SetActive(false);
        }
    }
    public void ToMenu()
    {
        if (gameState != menuState)
        {
            if (counterManager.countdownIsDone)
            {
                gameState = menuState;
                gameState.OnStateEnter();
                cameraScript.cameraRestored = false;
                commentatorScript.ChangeState(commentatorScript.inactiveState);
            }

        }
    }
    public void CheckForRoundWinner()
    {
        if(alivePlayers.Count == 1)
        {
            newRoundProcessStarted = true;
            foreach(GameObject player in alivePlayers)
            {
                roundWinner = player;
                player.GetComponent<PlayerScoreTracker>().IncrementScore();
            }         

        }
    }
    public void CheckForWinner()
    {
        //if roundwinner score == amount of rounds in game
        if (roundWinner.GetComponent<PlayerScoreTracker>().score == amountOfRounds)
        {
            gameState = winState;
            gameState.OnStateEnter();
            commentatorScript.victoryTrigger = true;
            commentatorScript.introducingTrigger = true;
        }
        else
        {
            OnStart();
        }
    }
    public void IncrementRounds()
    {
        amountOfRounds += 1;
    }
    public void DecrementRounds()
    {
        if(amountOfRounds > 1)
        {
            amountOfRounds -= 1;
        }
    }
    public void SetRoundsText()
    {
        roundsText.text = amountOfRounds.ToString();
    }


    public void TriggerMusicCheckpoints(float percentage)
    {
        if (counterManager.countdownIsDone)
        {
            if(percentage < firstHitTriggerValue)
            {
                audioManager.gameplayModeMusic.setParameterByName("firstDamage", 1);
                Debug.Log("first hit");
            }
            if(percentage <= halfHealthTriggerValue)
            {
                audioManager.gameplayModeMusic.setParameterByName("halfHealth", 1);
                Debug.Log("halfhp");
            }
        }
    }

    public void SetLowHealthMusic()
    {
        if (counterManager.countdownIsDone)
        {
            foreach (GameObject player in alivePlayers)
            {
                if (player.GetComponent<PlayerStatePattern>().health <=lowHealthTriggerValue)
                {
                    lowHPPlayer = true;
                    break;
                }
                else
                {
                    lowHPPlayer = false;
                }
            }
            if (lowHPPlayer == true)
            {
               audioManager.gameplayModeMusic.setParameterByName("lowHealth", 1);
            }
            else
            {
               audioManager.gameplayModeMusic.setParameterByName("lowHealth", 0);
            }
        }

    }

    public void ResetMusicParams()
    {
        audioManager.gameplayModeMusic.setParameterByName("firstDamage", 0);
        audioManager.gameplayModeMusic.setParameterByName("halfHealth", 0);
        audioManager.gameplayModeMusic.setParameterByName("lowHealth", 0);
    }
    public void DecrementCombinedHealth(float damage)
    {
        combinedCurrentHealth -= damage;
    }
    public float GetGlobalHealthPercentage()
    {
        return combinedCurrentHealth / combinedStartingHealth;
    }
}
