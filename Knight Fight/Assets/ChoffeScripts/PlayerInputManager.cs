using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputManager : MonoBehaviour
{
    public GameManager gm;
    public GameObject inputHandlerPrefab;
    public bool player1;
    public  PlayerInput player1InputHandler = null;
    public bool player2;
    public PlayerInput player2InputHandler = null;
    public bool player3;
    public PlayerInput player3InputHandler = null;
    public bool player4;
    public PlayerInput player4InputHandler = null;
    public bool trigger;
    public bool triggered;
    public List<PlayerInput> inputHandlers;

    private void Start()
    {
        //inputDevices = new List<InputDevice>();
        inputHandlers = new List<PlayerInput>();
        gm = GameObject.FindObjectOfType<GameManager>();
        //foreach (InputDevice index in InputSystem.devices)
        //    inputDevices.Add(index);
    }

    private void Update()
    {
        //if (trigger)
        //{
        //    if (!triggered)
        //    {
        //        SpawnPlayers();
        //        triggered = true;
        //    }
            
        //}
    }


    public void SpawnPlayers()
    {
        if (player1 == true && player1InputHandler == null)
        {
            player1InputHandler = PlayerInput.Instantiate(inputHandlerPrefab, 0, null,1, gm.inputDevices[0].device);
            inputHandlers.Add(player1InputHandler);
        }
        if (player2 == true && player2InputHandler == null)
        {
            player2InputHandler = PlayerInput.Instantiate(inputHandlerPrefab, 1, null, 1, gm.inputDevices[1].device);
            inputHandlers.Add(player2InputHandler);
        }
        if (player3 == true && player3InputHandler == null)
        {
            player3InputHandler = PlayerInput.Instantiate(inputHandlerPrefab, 2, null, 1, gm.inputDevices[2].device);
            inputHandlers.Add(player3InputHandler);
        }
        if (player4 == true && player4InputHandler == null)
        {
            player4InputHandler = PlayerInput.Instantiate(inputHandlerPrefab, 3, null, 1, gm.inputDevices[3].device);
            inputHandlers.Add(player4InputHandler);
        }

    }

    public void RemoveDevices()
    {
        inputHandlers.Clear();
        player1 = false;
        player1InputHandler = null;
        player2 = false;
        player2InputHandler = null;
        player3 = false;
        player3InputHandler = null;
        player4 = false;
        player4InputHandler = null;
    }
}
