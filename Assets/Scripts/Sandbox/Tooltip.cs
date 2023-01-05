using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Tooltip : MonoBehaviour
{
    TextMeshProUGUI cursorDisplay;

    [SerializeField] string thisObjectsTooltip = "";
    bool isEnabled;

    private void Awake()
    {
        isEnabled = !GameSession.MobileSession;

        if (isEnabled) cursorDisplay = GameObject.FindWithTag("Cursor Display").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isEnabled)
        {
            if (cursorDisplay.Equals(null))
            {
                Debug.LogError("This object wasn't properly connected with display text", gameObject);
            }
            else
            {
                GetToolTipMessage();
            } 
        } 
        //else Debug.Log("Tooltips Disabled");
    }

    void OnTriggerEnter2D()
    {
        if (isEnabled) cursorDisplay.text = thisObjectsTooltip;
    }

    void OnTriggerExit2D()
    {
        if (isEnabled) cursorDisplay.text = "";
    }

    void GetToolTipMessage()
    {
        string objectName = gameObject.name;
        
        if (objectName.Contains("(Clone)"))
        {
            objectName = objectName.Replace("(Clone)", "");
        }

        switch (objectName)
        {
            // COMPUTER OBJECTS
            case "Email":
            case "Email (Ghetto Prefab)":
                thisObjectsTooltip = "Answer email";
                break;
            case "Document":
            case "Document (Ghetto Prefab)":
                thisObjectsTooltip = "Approve document";
                break;

            // COMPUTER BUTTONS
            case "Email Button":
                thisObjectsTooltip = "Open email tab";
                break;
            case "Work Button":
                thisObjectsTooltip = "Open documents tab";
                break;
            case "Music Button":
                thisObjectsTooltip = "Toggle music";
                break;

            // ARROWS
            case "Left Arrow":
                thisObjectsTooltip = "Go left";
                break;
            case "Right Arrow":
                thisObjectsTooltip = "Go right";
                break;

            // DOG
            case "Dog Playing":
            case "Dog Relaxing":
            case "Dog Suspicious":
                thisObjectsTooltip = "Pet Toffee";
                break;
            case "Dog Sad":
                thisObjectsTooltip = "Comfort Toffee";
                break;
            case "Dog Sleeping":
                thisObjectsTooltip = "Toffee is fast asleep";
                break;
            case "Dog Toy Rope":
                thisObjectsTooltip = "Play with Toffee";
                break;

            // PHONE
            case "Phone":
            case "Phone Active":
                thisObjectsTooltip = "Answer call";
                break;

            default:
                thisObjectsTooltip = "Nothing was set :(";
                break;
        }
    }
}
