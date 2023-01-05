using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DocSpawner : MonoBehaviour
{
    // displayed in editor
    [Header("UI Components")]
    [SerializeField] GameObject disabledIconQueue;
    [SerializeField] GameObject enabledIconQueue;
    [SerializeField] TextMeshProUGUI queueText;

    [Header("Cached References")]
    [SerializeField] GameObject docPrefab;
    [SerializeField] List<Transform> docSlots;

    [Header("Sounds")]
    [SerializeField] AudioClip receivedSound;
    [SerializeField] AudioClip sentSound;

    [Space(10)]
    [SerializeField] int queueMax = 100;
    [SerializeField] string queueMaxReachedText = "99+";

    int _queueCurrent = 0;
    int _docsClicked = 0;
    int _docsTotal = 0;
    int _numOnScreen = 0;

    AudioSource _audioSource;

    void Awake()
    {
        Timer.GameOver += ExportScores;

        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _queueCurrent = docSlots.Count;
        _docsTotal = _queueCurrent;
        _numOnScreen = _queueCurrent;

        PopulateSlots();

        RefreshIcon();
    }

    private void PopulateSlots()
    {
        foreach (var slot in docSlots)
        {
            Instantiate(docPrefab, slot);
        }
    }

    void OnDestroy()
    {
        Timer.GameOver -= ExportScores;
    }

    void ExportScores() => FindObjectOfType<ScoreManager>().AddDocScore(_docsClicked, _docsTotal);

    #region Queue Logic
    public void DocClicked(Document document)
    {
        _docsClicked++;

        _queueCurrent--;

        if (_queueCurrent < docSlots.Count)
        {
            _numOnScreen--;

            Destroy(document.gameObject);
        }

        RefreshIcon();

        _audioSource.PlayOneShot(sentSound);
    }

    public IEnumerator AddToQueueWithDelay(int someNumber, float secsDelay)
    {
        yield return new WaitForSeconds(secsDelay);

        AddToQueue(someNumber);
    }

    public void AddToQueue(int someNumber)
    {
        if (_queueCurrent + someNumber > queueMax)
        {
            someNumber = queueMax - _queueCurrent;
        }

        _docsTotal += someNumber;

        _queueCurrent += someNumber;

        RefreshIcon();

        RefreshScreen(someNumber);

        if (!Computer.HasRouterIssues)
        {
            _audioSource.PlayOneShot(receivedSound);
        }
    }
    #endregion

    #region Refresh View Methods
    void RefreshIcon()
    {

        queueText.text = _queueCurrent >= queueMax
            ? queueMaxReachedText
            : _queueCurrent.ToString();

        if (_queueCurrent > 0)
        {
            disabledIconQueue.SetActive(true);
            enabledIconQueue.SetActive(true);
        }
        else
        {
            disabledIconQueue.SetActive(false);
            enabledIconQueue.SetActive(false);
            queueText.text = "";
        }
    }

    void RefreshScreen(int numAddedToQueue)
    {
        int numOfEmptySpaces = docSlots.Count - _numOnScreen;

        int numOfDocsToPrint = numAddedToQueue >= numOfEmptySpaces ? numOfEmptySpaces : numAddedToQueue;

        for (int i = 0; i < numOfDocsToPrint; i++)
        {
            foreach (Transform slot in docSlots)
            {
                if (slot.childCount < 1)
                {
                    Instantiate(docPrefab, slot);

                    _numOnScreen++;

                    break;
                }
            }
        }
    }
    #endregion
}