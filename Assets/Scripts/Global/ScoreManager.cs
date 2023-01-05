using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int _emailsClicked;
    [SerializeField] int _emailsTotal;
    [SerializeField] int _docsClicked;
    [SerializeField] int _docsTotal;
    [SerializeField] int _callsClicked;
    [SerializeField] int _callsTotal;
    [SerializeField] int _petsTotal;
    [SerializeField] int _unplugsTotal;

    public float EmailsPercent => (_emailsTotal != 0) ? (float)_emailsClicked / _emailsTotal * 100 : 0;

    public float DocsPercent => (_docsTotal != 0) ? (float)_docsClicked / _docsTotal * 100 : 0;

    public float CallsPercent => (_callsTotal != 0) ? (float)_callsClicked / _callsTotal * 100 : 0;
    public int CumulativeScore => _emailsClicked + _docsClicked + _callsClicked;
    public int CumulativePercent => (int)Mathf.Round((EmailsPercent + DocsPercent + CallsPercent) * 10000);

    private void Start()
    {
        SceneLoader.StartScene += ResetScores;
    }

    private void OnDestroy()
    {
        SceneLoader.StartScene -= ResetScores;
    }

    void ResetScores()
    {
        _emailsClicked = 0;
        _emailsTotal = 0;

        _docsClicked = 0;
        _docsTotal = 0;

        _callsClicked = 0;
        _callsTotal = 0;

        _petsTotal = 0;
        _unplugsTotal = 0;
    }

    #region Importing Methods
    public void AddEmailScore(int emailClicked, int emailsTotal)
    {
        _emailsClicked = emailClicked;

        _emailsTotal = emailsTotal;
    }

    public void AddDocScore(int docsClicked, int docsTotal)
    {
        _docsClicked = docsClicked;

        _docsTotal = docsTotal;
    }

    public void AddPhoneScore(int callsClicked, int callsTotal)
    {
        _callsClicked = callsClicked;

        _callsTotal = callsTotal;
    }

    public void AddPetScore(int petsTotal)
    {
        _petsTotal = petsTotal;
    }

    public void AddUnplugScore(int unplugsTotal)
    {
        _unplugsTotal = unplugsTotal;
    }
    #endregion

    #region Formatting Methods
    public string DisplayEmailScore() => _emailsClicked + " of " + _emailsTotal;
    public string DisplayEmailPercent() => "(" + string.Format("{0:0.0}", EmailsPercent) + "%)";
    public string DisplayDocScore() => _docsClicked + " of " + _docsTotal;
    public string DisplayDocPercent() => "(" + string.Format("{0:0.0}", DocsPercent) + "%)";
    public string DisplayCallScore() => _callsClicked + " of " + _callsTotal;
    public string DisplayCallPercent() => "(" + string.Format("{0:0.0}", CallsPercent) + "%)";
    public string DisplayPetScore() => (_petsTotal == 1) ? _petsTotal + " time" : _petsTotal + " times";
    public string DisplayUnplugScore() => (_unplugsTotal == 1) ? _unplugsTotal + " time" : _unplugsTotal + " times";
    public string DisplayForLeaderboard() => string.Format("{0}${1}${2}${3}${4}${5}${6}${7}",
        DisplayEmailScore(), DisplayEmailPercent(),
        DisplayDocScore(), DisplayDocPercent(),
        DisplayCallScore(), DisplayCallPercent(),
        DisplayPetScore(),
        DisplayUnplugScore()).Replace(' ', '_').Replace("%", "percent");

    public string FormatName(string playerName)
    {
        return playerName.Replace(' ', '_');
    }
    #endregion
}