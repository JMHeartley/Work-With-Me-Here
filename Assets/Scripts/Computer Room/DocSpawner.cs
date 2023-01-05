using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DocSpawner : MonoBehaviour
{
    [Header("Cached Game UI Components")]
    [SerializeField] GameObject disabledIconQueue;
    [SerializeField] GameObject enabledIconQueue;
    [SerializeField] TextMeshProUGUI queueText;

    [Header("Cached References")]
    [SerializeField] GameObject docPrefab;
    [SerializeField] List<Transform> docSlots;

    [Header("Sounds")]
    [SerializeField] AudioClip receivedSound;
    [SerializeField] AudioClip sentSound;

    // private variables
    int queueCurrent = 0;
    int numOfDocsOnScreen = 0;

    // cached components
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        queueCurrent = docSlots.Count;

        numOfDocsOnScreen = queueCurrent;
        
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
    public void DocClicked(Document document)
    {

        queueCurrent--;

        if (queueCurrent >= docSlots.Count)
        {
            //replace email in that spot
            PrintDoc(document.transform.parent);
        }

        RefreshIcon();

        numOfDocsOnScreen--;

        Destroy(document.gameObject);

        //audioSource.PlayOneShot(sentSound);
        audioSource.PlayOneShot(receivedSound);
    }

    public void AddToQueue(int someNumber)
    {
        if (someNumber > 0)
        {
            //Debug.Log(someNumber + " added to the doc queue!");

            queueCurrent += someNumber;

            RefreshIcon();

            RefreshScreen(someNumber);

            //audioSource.PlayOneShot(receivedSound);
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

        int numOfEmptySpaces = docSlots.Count - numOfDocsOnScreen;

        int numOfDocsToPrint = 0;

        if (numAddedToQueue >= numOfEmptySpaces)
        {
            numOfDocsToPrint = numOfEmptySpaces;
        }
        else
        {
            numOfDocsToPrint = numAddedToQueue;
        }

        for (int i = 0; i < numOfDocsToPrint; i++)
        {
            foreach (Transform slot in docSlots)
            {
                if (slot.childCount < 1)
                {
                    //Debug.Log(slot.name + " is an empty slot!");

                    PrintDoc(slot);

                    break;
                }
            }
        }

        //Debug.Log(numOfEmptySpaces + " spots were empty on the screen!");
        //Debug.Log(numOfEmailsToPrint + " emails were printed to the screen!");
    }

    void PrintDoc(Transform parent)
    {
        numOfDocsOnScreen++;

        Instantiate(docPrefab, parent);
    }
}