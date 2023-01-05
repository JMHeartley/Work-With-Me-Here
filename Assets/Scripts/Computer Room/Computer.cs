using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Different Screens")]
    [SerializeField] GameObject mainScreen;
    [SerializeField] GameObject noConnectScreen;
    [SerializeField] GameObject reConnectScreen;

    //private varables
    GameObject[] canvases;

    // Start is called before the first frame update
    void Awake()
    {
        canvases = GameObject.FindGameObjectsWithTag("Computer Canvas");

        DisplayMain();
    }
    private void Start()
    {
        RoomChanger.RoomChanged += ActivateRoomCanvases;
    }

    private void OnDestroy()
    {
        RoomChanger.RoomChanged -= ActivateRoomCanvases;
    }

    public void DisplayMain()
    {
        mainScreen.SetActive(true);
        noConnectScreen.SetActive(false);
        reConnectScreen.SetActive(false);
    }

    public void DisplayNoConnect()
    {
        mainScreen.SetActive(false);
        noConnectScreen.SetActive(true);
        reConnectScreen.SetActive(false);
    }

    public void DisplayReConnect()
    {
        mainScreen.SetActive(false);
        noConnectScreen.SetActive(false);
        reConnectScreen.SetActive(true);
    }

    void ActivateRoomCanvases()
    {
        if (transform.localPosition.x == 25)
        {
            foreach (GameObject canvas in canvases)
            {
                canvas.SetActive(false);
            }
        }
        else if (transform.localPosition.x == 0)
        {
            foreach (GameObject canvas in canvases)
            {
                canvas.SetActive(true);
            }
        }
    }
}