using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] int offScreenPosition = 25;

    [Header("Controling Arrows")]
    [SerializeField] GameObject leftArrow;
    [SerializeField] GameObject rightArrow;

    [Header("Room Organization")]
    [SerializeField] int startingRoomIndex;
    [SerializeField] GameObject[] rooms;

    // public events
    public delegate void RoomAction();
    public static event RoomAction RoomChanged;

    public string ActiveRoomName { get { return rooms[activeRoom].name; } }

    // private variables
    int activeRoom;
    GameObject[] leftSounds;
    GameMusic gameMusic;

    void Awake()
    {
        leftSounds = GameObject.FindGameObjectsWithTag("Left Sounds");

        gameMusic = FindObjectOfType<GameMusic>();

        Camera.main.transform.position = new Vector3 (0f, 0f, Camera.main.transform.position.z);
    }

    void Start()
    {
        activeRoom = startingRoomIndex;

        InitRoom();
    }
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");

        if (Mathf.Abs(hori) > 0f)
        {
            // right is positive, left is negative
            if (Mathf.Sign(hori) == 1) GoRight();
            else GoLeft();
        }
    }

    void InitRoom()
    {
        SetActiveRoom();

        if (startingRoomIndex == 0)
        {
            DisableLeftArrow();
        }
        else if (startingRoomIndex == rooms.Length - 1)
        {
            DisableRightArrow();
        }
    }

    public void GoLeft()
    {
        if (activeRoom > 0)
        {
            activeRoom--;

            if (activeRoom == 0)
            {
                DisableLeftArrow();
            }

            EnableRightArrow();

            SetActiveRoom();

            gameMusic.Both(); //temp solution: would be in computer room for now

            ChangeLeftSounds(0);
        }
    }

    public void GoRight()
    {
        if (activeRoom < rooms.Length - 1)
        {
            activeRoom++;

            if (activeRoom == rooms.Length - 1)
            {
                DisableRightArrow();
            }

            EnableLeftArrow();

            SetActiveRoom();

<<<<<<< Updated upstream:Assets/Scripts/RoomChanger.cs
=======
            gameMusic.Left(); //temp solution: would be computer room is on left for now

            ChangeLeftSounds(-0.8f);
        }
    }

    // private methods

    void InitRoom()
    {
        SetActiveRoom();

        if (startingRoomIndex == 0)
        {
            DisableLeftArrow();
        }
        else if (startingRoomIndex == rooms.Length - 1)
        {
            DisableRightArrow();
        }
    }

>>>>>>> Stashed changes:Assets/Scripts/Sandbox/RoomChanger.cs
    void EnableLeftArrow()
    {
        leftArrow.SetActive(true);
    }

    void EnableRightArrow()
    {
        rightArrow.SetActive(true);
    }

    void DisableLeftArrow()
    {
        leftArrow.SetActive(false);
    }

    void DisableRightArrow()
    {
        rightArrow.SetActive(false);
    }

    void SetActiveRoom()
    {
        
        foreach (GameObject Room in rooms)
        {
            //Room.SetActive(false);
            Room.transform.localPosition = new Vector3(
                offScreenPosition,
                Room.transform.localPosition.y,
                Room.transform.localPosition.z);
        }

        //rooms[activeRoom].SetActive(true);
        rooms[activeRoom].transform.localPosition = new Vector3(
                0,
                rooms[activeRoom].transform.localPosition.y,
                rooms[activeRoom].transform.localPosition.z);

        RoomChanged?.Invoke();
    }

    void ChangeLeftSounds(float panStereoValue)
    {
        foreach (var sound in leftSounds)
        {
            sound.GetComponent<AudioSource>().panStereo = panStereoValue;
        }
    }
}
