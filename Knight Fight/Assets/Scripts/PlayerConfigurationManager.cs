using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Any scene can acces this class to get different player configs (what color player is, number and player input)
public class PlayerConfigurationManager : MonoBehaviour
{
    //A property of this class
    private List<PlayerConfiguration> playerConfigs;

    //Control how many players we need ready in order to advance to the next scene.
    [SerializeField]
    private int MaxPlayers = 4;

    //Singletonpattern = Allows us to access this class/single instance from any gameobject, any scene, as long it's active, makes sure that only one instance of this class is active at a time.
    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to create another instance of singleton!!");
        }  
        else
        {
            Instance = this;
            //Moves the object to a special scene that Unity engine uses to persist gameobjects across different scenes.
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();    //initialize our playerConfigs
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    //Method for player to set their colors
    public void SetPlayerColor(int index, Material color)
    {
        //Takes the local playerConfigs, sets playerMaterial on that object (depending on index number) to be the color we're passing in.
        playerConfigs[index].PlayerMaterial = color;
    }

    //See which current player is ready
    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        //Checks if all the players are ready
        if (playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))    //Checks the player index to make sure if we haven't already added this player.
        {
            pi.transform.SetParent(transform);  //Set the player inputs transform to our transform so it'll become a child of "Player Configuration Manager" object. 
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    //Constructor
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }   //After player has picked color and clicked ready, their ready to load into the next "screen".
    
    public Material PlayerMaterial { get; set; }
}
