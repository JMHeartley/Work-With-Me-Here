using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerButton : MonoBehaviour
{
    ComputerButtons computerButtons;

    private void Awake()
    {
        computerButtons = FindObjectOfType<ComputerButtons>();
    }

    private void OnMouseDown()
    {
        computerButtons.ButtonHandler(transform.name);
    }
}
