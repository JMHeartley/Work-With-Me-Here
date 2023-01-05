using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(Rigidbody2D))]

public class Cursor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cursorDisplay;

    [Tooltip("Cursor display vertical offset position")]
    [SerializeField] float displayOffsetUpper = 1f;
    [Tooltip("Cursor display vertical offset position")]
    [SerializeField] float displayOffsetLower = 0.75f;

    [Tooltip("Cursor display on top when below this % of window")]
    [SerializeField] float viewportCutOff = 0.09f;

    private void Start()
    {
        cursorDisplay.text = "";

        if (GameSession.MobileSession)
        {
            Destroy(gameObject);
            Destroy(cursorDisplay.gameObject);
        }
    }

    void Update()
    {
        FollowMousePosition();

        //Vector3 cursorPosWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("Cursor X: " + cursorPosWorldPoint.x);
        //Debug.Log("Cursor Y: " + cursorPosWorldPoint.y);
    }

    private void FollowMousePosition()
    {
        Vector3 cursorPosWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(cursorPosWorldPoint.x, cursorPosWorldPoint.y, transform.position.z);

        float newCursorPosY = Camera.main.ScreenToViewportPoint(Input.mousePosition).y < viewportCutOff
            ? cursorPosWorldPoint.y + displayOffsetLower
            : cursorPosWorldPoint.y - displayOffsetUpper;

        cursorDisplay.transform.position = new Vector3(cursorPosWorldPoint.x, newCursorPosY, cursorDisplay.transform.position.z);
    }
}