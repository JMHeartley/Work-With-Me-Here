using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButtons : MonoBehaviour
{
    [Header("Computer Tabs")]
    [SerializeField] GameObject emailTab;
    [SerializeField] GameObject workTab;
    [SerializeField] GameObject musicTab;

    //[Header("Music")]
    //[SerializeField] GameObject musicSource1;
    //[SerializeField] GameObject musicSource2;

    //AudioSource song1;
    //AudioSource song2;

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
                Debug.Log("SOME OTHER COMPUTER BUTTON WAS TOGGLED");
                Debug.Log(buttonName);
                break;
        }        
    }

    private void Start()
    {
        ToggleEmail();

        //song1 = musicSource1.GetComponent<AudioSource>();
        //song2 = musicSource2.GetComponent<AudioSource>();
    }

    private void ToggleEmail()
    {
        emailTab.SetActive(true);
        workTab.SetActive(false);
    }

    private void ToggleWork()
    {
        emailTab.SetActive(false);
        workTab.SetActive(true);
    }

    private void ToggleMusic()
    {
        musicTab.SetActive(!musicTab.activeSelf);

        //song1.mute = !song1.mute;
        
        //song2.mute = !song2.mute;
    }
}
