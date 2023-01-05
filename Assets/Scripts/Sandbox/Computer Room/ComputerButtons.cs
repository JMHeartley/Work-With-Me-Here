using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButtons : MonoBehaviour
{
    [Header("Computer Tabs")]
    [SerializeField] GameObject _emailTab;
    [SerializeField] GameObject _workTab;
    [SerializeField] GameObject _musicTab;

    private void Start()
    {
        ToggleEmail();

        _musicTab.SetActive(true);
    }

    public void ButtonHandler(string buttonName)
    {
        switch (buttonName)
        {
            case "Email Button":
                ToggleEmail();
                break;

            case "Work Button":
                ToggleWork();
                break;

            case "Music Button":
                ToggleMusic();
                break;
            default:
                Debug.LogError("SOME OTHER COMPUTER BUTTON WAS TOGGLED: " + buttonName);
                break;
        }        
    }

    private void ToggleEmail()
    {
        _emailTab.SetActive(true);
        _workTab.SetActive(false);
    }

    private void ToggleWork()
    {
        _emailTab.SetActive(false);
        _workTab.SetActive(true);
    }

    private void ToggleMusic()
    {
        _musicTab.SetActive(!_musicTab.activeSelf);

        FindObjectOfType<GameMusic>().TogglePauseGameplaySong();
    }
}
