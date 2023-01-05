using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    RoomChanger roomChanger;

    // Start is called before the first frame update
    private void Awake()
    {
        roomChanger = FindObjectOfType<RoomChanger>();
    }

    private void OnMouseDown()
    {
        if (transform.name == "Left Arrow")
        {
            Debug.Log("Left");

            roomChanger.GoLeft();
        }
        else if (transform.name == "Right Arrow")
        {
            Debug.Log("Right");

            roomChanger.GoRight();
        }
    }
}