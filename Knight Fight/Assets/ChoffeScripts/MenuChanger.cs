using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuChanger : MonoBehaviour
{
    public EventSystem eventSystem;

    public GameObject OptionsMenu;
    public GameObject OptionsMenuDefaultSelected;


    public GameObject GameMenu;
    public GameObject GameMenuDefaultSelected;

    public void ChangeToOptions()
    {
        OptionsMenu.SetActive(true);
        GameMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(OptionsMenuDefaultSelected);
    }

    public void ChangeToMenu()
    {
        GameMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        eventSystem.SetSelectedGameObject(GameMenuDefaultSelected);
    }
}
