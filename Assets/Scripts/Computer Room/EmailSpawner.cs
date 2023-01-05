using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmailSpawner : MonoBehaviour
{
    [Header("Cached Game UI Components")]
    [SerializeField] GameObject disabledIconQueue;
    [SerializeField] GameObject enabledIconQueue;
    [SerializeField] TextMeshProUGUI queueText;

    [Header("Cached References")]
    [SerializeField] GameObject emailPrefab;
    [SerializeField] List<Transform> emailSlots;

    [Header("Sounds")]
    [SerializeField] AudioClip receivedSound;
    [SerializeField] AudioClip sentSound;

    // private variables
    int queueCurrent = 0;
    int numOfEmailsOnScreen = 0;

    // cached components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        queueCurrent = emailSlots.Count;

        numOfEmailsOnScreen = queueCurrent;

        RefreshIcon();
    }

    // Update is called once per frame
    void Update()
    {
        //debugging purposes
        if (Input.GetButtonDown("Jump")) // space
        {
            AddToQueue(1);
        }
        //debugging purposes
        if (Input.GetButtonDown("Fire2")) // space
        {
            AddToQueue(2);
            //RefreshScreen();
        }
    }

    // public for call by Email objects
    public void EmailClicked(Email email)
    {

        queueCurrent--;

        if (queueCurrent >= emailSlots.Count)
        {
            //replace email in that spot
            PrintEmail(email.transform.parent);
        }

        RefreshIcon();

        numOfEmailsOnScreen--;

        Destroy(email.gameObject);
        
        audioSource.PlayOneShot(sentSound);
    }

    public void AddToQueue(int someNumber)
    {
        if (someNumber > 0)
        {
            //Debug.Log(someNumber + " added to the queue!");

            queueCurrent += someNumber;

            FindObjectOfType<DocSpawner>().AddToQueue(queueCurrent);

            RefreshIcon();

            RefreshScreen(someNumber);

            audioSource.PlayOneShot(receivedSound);
        }
    }

    ////// private methods

    void RefreshIcon()
    {
        if (queueCurrent > 0)
        {
            disabledIconQueue.SetActive(true);
            enabledIconQueue.SetActive(true);
        }
        else
        {
            disabledIconQueue.SetActive(false);
            enabledIconQueue.SetActive(false);
        }

        queueText.text = queueCurrent.ToString();
    }

    void RefreshScreen(int numAddedToQueue)
    {

        int numOfEmptySpaces = emailSlots.Count - numOfEmailsOnScreen;

        int numOfEmailsToPrint = 0;
        
        if (numAddedToQueue >= numOfEmptySpaces)
        {
            numOfEmailsToPrint = numOfEmptySpaces;
        }
        else
        {
            numOfEmailsToPrint = numAddedToQueue;
        }

        for (int i = 0; i < numOfEmailsToPrint; i++)
        {
            foreach (Transform slot in emailSlots)
            {
                if (slot.childCount < 1)
                {
                    //Debug.Log(slot.name + " is an empty slot!");

                    PrintEmail(slot);

                    break;
                }
            }
        }
        
        //Debug.Log(numOfEmptySpaces + " spots were empty on the screen!");
        //Debug.Log(numOfEmailsToPrint + " emails were printed to the screen!");
    }

    void PrintEmail(Transform parent)
    {
        numOfEmailsOnScreen++;

        Instantiate(emailPrefab, parent);
    }
}