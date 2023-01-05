using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SubmitName : MonoBehaviour
{

    [SerializeField] TMP_InputField playerName;
    [SerializeField] TextMeshProUGUI errorDisplay;
    [SerializeField] GameObject submitButton;
    [SerializeField] GameObject leaderboardButton;

    [SerializeField] GameObject leaderboardScreen;

    bool isSubmitted;
    TouchScreenKeyboard touchScreenKeyboard;
    public static string TimeStamp { get; private set; }

    bool HasValidName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(playerName.text))
            {
                errorDisplay.text = "Name is blank :(";
                return false;
            }

            Regex unsafeChars = new Regex(@"[^a-zA-Z0-9\s]");

            if (unsafeChars.IsMatch(playerName.text))
            {
                errorDisplay.text = "Please only use letters or numbers :S";
                return false;
            }

            return true;
        }
    }

    ScoreManager scoreManager;
    dreamloLeaderBoard dreamlo;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        dreamlo = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
    }
    void Start()
    {
        errorDisplay.text = "";

        submitButton.SetActive(true);
        leaderboardButton.SetActive(false);

        TimeStamp = string.Format("{0:yyMMddHHmmss}", DateTime.Now.ToUniversalTime());        
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit")) // backspace
        {
            Submit();
        }
    }

    public void Submit()
    {
        if (isSubmitted)
        {
            leaderboardScreen.SetActive(!leaderboardScreen.activeSelf);
        }
        else if (HasValidName)
        {
            StartCoroutine(SubmitScores());
        }
    }

    public IEnumerator SubmitScores()
    {
        errorDisplay.text = "Submitting scores...";

        errorDisplay.transform.localPosition = playerName.transform.localPosition;
        playerName.gameObject.SetActive(false);
        submitButton.GetComponent<Button>().interactable = false;

        StartCoroutine(dreamlo.AddScoreRetrieveCouroutine(TimeStamp,
                            scoreManager.CumulativePercent,
                            scoreManager.CumulativeScore,
                            scoreManager.DisplayForLeaderboard() + "$" +
                            scoreManager.FormatName(playerName.text)));

        while (dreamlo.HasEmptyScores)
        {
            yield return null;
        }

        errorDisplay.text = "Scores submitted! :D";

        submitButton.SetActive(false);
        leaderboardButton.SetActive(true);

        isSubmitted = true;
    }
}