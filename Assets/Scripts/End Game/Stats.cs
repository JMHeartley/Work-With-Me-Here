using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Stats : MonoBehaviour
{
    // diplayed in editor
    [Header("Displays")]
    [SerializeField] TextMeshProUGUI emailDisplay;
    [SerializeField] TextMeshProUGUI emailPercentDisplay;
    [Space(10)]

    [SerializeField] TextMeshProUGUI docDisplay;
    [SerializeField] TextMeshProUGUI docPercentDisplay;
    [Space(10)]

    [SerializeField] TextMeshProUGUI callDisplay;
    [SerializeField] TextMeshProUGUI callPercentDisplay;
    [Space(10)]

    [SerializeField] TextMeshProUGUI petDisplay;
    [SerializeField] TextMeshProUGUI routerDisplay;

    [Header("Colors")]
    [SerializeField] Color32 goodColor;
    [SerializeField] Color32 badColor;

    // cached references
    ScoreManager scoreManager;

    bool debugEnabled;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Start()
    {
        debugEnabled = FindObjectOfType<GameSession>().insertFakeScores;
        if (debugEnabled)
        {
            Debug.Log("Fake scores inserted!");
            InsertFakeScores();
        }
        
        DisplayEndGameStats();

        SetStatColor();
    }

    void InsertFakeScores()
    {
        int x = 0;
        int y = 0;

        RandomPairs();
        scoreManager.AddEmailScore(x, y);

        RandomPairs();
        scoreManager.AddDocScore(x, y);

        RandomPairs();
        scoreManager.AddPhoneScore(x, y);

        RandomPairs();
        scoreManager.AddPetScore(y);

        RandomPairs();
        scoreManager.AddUnplugScore(y);
        
        void RandomPairs()
        {
            x = UnityEngine.Random.Range(50, 75);

            y = x + UnityEngine.Random.Range(0, 16);
        }
    }

    void DisplayEndGameStats()
    {
        emailDisplay.text = scoreManager.DisplayEmailScore();
        emailPercentDisplay.text = scoreManager.DisplayEmailPercent();
        
        docDisplay.text = scoreManager.DisplayDocScore();
        docPercentDisplay.text = scoreManager.DisplayDocPercent();
        
        callDisplay.text = scoreManager.DisplayCallScore();
        callPercentDisplay.text = scoreManager.DisplayCallPercent();

        petDisplay.text = string.Format("You pet Toffee {0}", scoreManager.DisplayPetScore());
        routerDisplay.text = string.Format("Router was unplugged {0}", scoreManager.DisplayUnplugScore());

    }

    void SetStatColor()
    {
        SetColor(scoreManager.EmailsPercent, emailDisplay, emailPercentDisplay);
        SetColor(scoreManager.DocsPercent, docDisplay, docPercentDisplay);
        SetColor(scoreManager.CallsPercent, callDisplay, callPercentDisplay);

        void SetColor(float percent, TextMeshProUGUI display, TextMeshProUGUI percentDisplay)
        {
            if (percent > 50)
            {
                display.color = goodColor;
                percentDisplay.color = goodColor;
            }
            else
            {
                display.color = badColor;
                percentDisplay.color = badColor;
            }
        }
    }


}