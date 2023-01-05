using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EmailSpawner : MonoBehaviour
{
    // displayed in editor
    [Header("UI Components")]
    [SerializeField] GameObject disabledIconQueue;
    [SerializeField] GameObject enabledIconQueue;
    [SerializeField] TextMeshProUGUI queueText;

    [Header("Cached References")]
    [SerializeField] GameObject emailPrefab;
    [SerializeField] List<Transform> emailSlots;

    [Header("Sounds")]
    [SerializeField] AudioClip receivedSound;
    [SerializeField] AudioClip sentSound;

    [Space(10)]
    [SerializeField] int queueMax = 100;
    [SerializeField] string queueMaxReachedText = "99+";

    int _queueCurrent = 0;
    int _emailsClicked = 0;
    int _emailsTotal = 0;
    int _numOnScreen = 0;

    AudioSource _audioSource;

    public int CurrentQueue { get { return _queueCurrent; } }

    void Awake()
    {
        Timer.GameOver += ExportScores;

        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _queueCurrent = emailSlots.Count;
        _emailsTotal = emailSlots.Count;
        _numOnScreen = emailSlots.Count;

        PopulateSlots();

        RefreshIcon();
    }

    private void PopulateSlots()
    {
        foreach (var slot in emailSlots)
        {
            Instantiate(emailPrefab, slot);
        }
    }

    void OnDestroy()
    {
        Timer.GameOver -= ExportScores;
    }

    void ExportScores() => FindObjectOfType<ScoreManager>().AddEmailScore(_emailsClicked, _emailsTotal);

    #region Queue Logic
    public void EmailClicked(Email email)
    {
        _emailsClicked++;

        _queueCurrent--;

        if (_queueCurrent < emailSlots.Count)
        {
            _numOnScreen--;

            Destroy(email.gameObject);
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

        _emailsTotal += someNumber;

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
        int numOfEmptySpaces = emailSlots.Count - _numOnScreen;

        int numOfEmailsToPrint = numAddedToQueue >= numOfEmptySpaces ? numOfEmptySpaces : numAddedToQueue;

        for (int i = 0; i < numOfEmailsToPrint; i++)
        {
            foreach (Transform slot in emailSlots)
            {
                if (slot.childCount < 1)
                {
                    Instantiate(emailPrefab, slot);

                    _numOnScreen++;

                    break;
                }
            }
        }
    }
    #endregion
}