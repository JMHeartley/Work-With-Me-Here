using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Cached References")]
    [SerializeField] TextMeshProUGUI timerDisplay;

    [SerializeField] EmailSpawner emailSpawner;
    [SerializeField] DocSpawner docSpawner;
    
    [Header("Configurable Parameters")]
    [Tooltip("Measured in seconds")]
    [SerializeField] int gameDuration = 180;
    [SerializeField] float gameStartDelay = 0.5f;

    // public events
    public delegate void PhoneAction();
    public static event PhoneAction PhoneStartRinging;
    
    // private variables
    float tick = 1f;
    float lastTimeStamp;

    int[] Odds1 = { 0, 0, 0, 1, 1 }; // <150
    int[] Odds2 = { 0, 0, 1, 1, 1 }; // <120
    int[] Odds3 = { 0, 1, 1, 1, 2 }; // <90
    int[] Odds4 = { 0, 0, 1, 1, 1 }; // <60
    int[] Odds5 = { 0, 0, 0, 1, 1 }; // <30

    private void Start()
    {
        StartCoroutine(PhoneRingCountdown());

        timerDisplay.text = gameDuration.ToString();

        InvokeRepeating("Tick", gameStartDelay, tick);
    }

    void Tick()
    {
        gameDuration--;

        timerDisplay.text = gameDuration.ToString();

        PushToEmail();
    }

    void PushToEmail()
    {
        int x = Random.Range(0, 5);

        int[] oddsTemp = { };

        if (gameDuration > 150)
        {
            emailSpawner.AddToQueue(Odds1[x]);
        }
        else if (gameDuration > 120)
        {
            emailSpawner.AddToQueue(Odds2[x]);
        }
        else if (gameDuration > 90)
        {
            emailSpawner.AddToQueue(Odds3[x]);
        }
        else if (gameDuration > 60)
        {
            emailSpawner.AddToQueue(Odds4[x]);
        }
        else
        {
            emailSpawner.AddToQueue(Odds5[x]);
        }
    }

    IEnumerator PhoneRingCountdown()
    {
        int x = 7 + Random.Range(0, 8);

        //Debug.Log("Waiting for " + x + " seconds to ring");

        yield return new WaitForSeconds(x);

        PhoneStartRinging?.Invoke();

        StartCoroutine(PhoneDelay());
    }

    IEnumerator PhoneDelay()
    {
        yield return new WaitForSeconds(14f);

        StartCoroutine(PhoneRingCountdown());
    }
}