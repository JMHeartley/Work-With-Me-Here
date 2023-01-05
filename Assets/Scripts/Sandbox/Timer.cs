using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    // diplayed in editor
    [Header("Cached References")]
    [SerializeField] EmailSpawner emailSpawner;
    [SerializeField] DocSpawner docSpawner;

    [Header("Configurable Parameters")]
    [Tooltip("Measured in seconds")]
    [SerializeField] int gameDurationMax = 180;
    [SerializeField] float gameStartDelay = 0.5f;
    [SerializeField]
    [Range(0, 1)] float addQueueBuffer = 0.5f;

    [Header("Bar Sprite")]
    [SerializeField] GameObject timerBar = null;

    [Header("Bar Colors")]
    [SerializeField] Color startingColor;
    [SerializeField] Color warningColor;
    [SerializeField] Color shortTimeColor;

    [Header("Clock")]
    [SerializeField] Animator clockAnimator;

    // public events
    public delegate void GameAction();
    public static event GameAction GameOver;

    // private variables
    float tick = 1f;
    float lastTimeStamp = 0f;

    int gameDuration = 0;

    float shrinkFactor = 0f;

    bool debugEnabled;

    SpriteRenderer barSprite;

    int perSeconds0;
    int perSeconds1;
    int perSeconds2;
    int perSeconds3;
    int perSeconds4;
    int perSeconds5;
    int perSecondsDisconn;

    int[] Odds0; // 180 - 151
    int[] Odds1; // 150 - 121
    int[] Odds2; // 120 - 91
    int[] Odds3; // 90 - 61
    int[] Odds4; // 60 - 30
    int[] Odds5; // 30 - 0
    int[] OddsDisconn; // when disconnected

    Dog dog;

    void Awake()
    {
        barSprite = timerBar.GetComponent<SpriteRenderer>();
        dog = FindObjectOfType<Dog>();
        debugEnabled = FindObjectOfType<GameSession>().timerDebug;

    }

    private void Start()
    {
        gameDuration = gameDurationMax;

        FindObjectOfType<GameMusic>().PlayGamplaySong();

        CalculateBarShrinkFactor();

        StartCoroutine(UpdateOddsWithDelay());

        clockAnimator.SetTrigger("On");

        InvokeRepeating("Tick", gameStartDelay, tick);
    }

    void Update()
    {
        if (Input.GetButtonDown("Debug 1") && debugEnabled) // backspace
        {
            gameDuration -= 30;

            Debug.Log("Debug 1 button pressed: " + gameDuration + " seconds left");
        }
    }

    #region Game Timer Logic
    private void Tick()
    {
        gameDuration--;

        PushToEmail();

        dog.DogTimer();

        UpdateBarColor();

        UpdateBarLength();

        if (gameDuration <= 0)
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        Debug.Log("Game Ended");

        CancelInvoke("Tick");

        GameOver?.Invoke();

        yield return new WaitForSeconds(1f);

        FindObjectOfType<SceneLoader>().LoadNextTransition();

    }
    #endregion

    #region Odds Methods

    IEnumerator UpdateOddsWithDelay()
    {
        yield return new WaitForSeconds(gameStartDelay - 0.25f);
        UpdateOdds();
    }
    public void UpdateOdds()
    {
        var difficulty = FindObjectOfType<GameSession>().SessionDifficulty;

        var oddsRates = difficulty.Rates;

        perSeconds0 = oddsRates[0];
        perSeconds1 = oddsRates[1];
        perSeconds2 = oddsRates[2];
        perSeconds3 = oddsRates[3];
        perSeconds4 = oddsRates[4];
        perSeconds5 = oddsRates[5];
        perSecondsDisconn = oddsRates[6];

        var oddsArrayOfArrays = difficulty.Odds;

        Odds0 = new int[] { oddsArrayOfArrays[0][0], oddsArrayOfArrays[0][1], oddsArrayOfArrays[0][2], oddsArrayOfArrays[0][3], oddsArrayOfArrays[0][4] };
        Odds1 = new int[] { oddsArrayOfArrays[1][0], oddsArrayOfArrays[1][1], oddsArrayOfArrays[1][2], oddsArrayOfArrays[1][3], oddsArrayOfArrays[1][4] };
        Odds2 = new int[] { oddsArrayOfArrays[2][0], oddsArrayOfArrays[2][1], oddsArrayOfArrays[2][2], oddsArrayOfArrays[2][3], oddsArrayOfArrays[2][4] };
        Odds3 = new int[] { oddsArrayOfArrays[3][0], oddsArrayOfArrays[3][1], oddsArrayOfArrays[3][2], oddsArrayOfArrays[3][3], oddsArrayOfArrays[3][4] };
        Odds4 = new int[] { oddsArrayOfArrays[4][0], oddsArrayOfArrays[4][1], oddsArrayOfArrays[4][2], oddsArrayOfArrays[4][3], oddsArrayOfArrays[4][4] };
        Odds5 = new int[] { oddsArrayOfArrays[5][0], oddsArrayOfArrays[5][1], oddsArrayOfArrays[5][2], oddsArrayOfArrays[5][3], oddsArrayOfArrays[5][4] };
        OddsDisconn = new int[] { oddsArrayOfArrays[6][0], oddsArrayOfArrays[6][1], oddsArrayOfArrays[6][2], oddsArrayOfArrays[6][3], oddsArrayOfArrays[6][4] };

    }

    #endregion

    #region Queue Logic
    private void PushToEmail()
    {
        int x = Random.Range(0, 5);

        if (Computer.HasRouterIssues)
        {
            if (gameDuration % perSecondsDisconn == 0)
            {
                SendToQueues(OddsDisconn[x], "OddsDisconn");
            }
        }
        else
        {
            if (gameDuration > (gameDurationMax * 5 / 6))
            {
                if (gameDuration % perSeconds0 == 0)
                {
                    SendToQueues(Odds0[x], "Odds0");
                }
            }
            else if (gameDuration > (gameDurationMax * 4 / 6))
            {
                if (gameDuration % perSeconds1 == 0)
                {
                    SendToQueues(Odds1[x], "Odds1");
                }
            }
            else if (gameDuration > (gameDurationMax * 3 / 6))
            {
                if (gameDuration % perSeconds2 == 0)
                {
                    SendToQueues(Odds2[x], "Odds2");
                }
            }
            else if (gameDuration > (gameDurationMax * 2 / 6))
            {
                if (gameDuration % perSeconds3 == 0)
                {
                    SendToQueues(Odds3[x], "Odds3");
                }
            }
            else if (gameDuration > (gameDurationMax * 1 / 6))
            {
                if (gameDuration % perSeconds4 == 0)
                {
                    SendToQueues(Odds4[x], "Odds4");
                }
            }
            else if (gameDuration > 2)
            {
                if (gameDuration % perSeconds5 == 0)
                {
                    SendToQueues(Odds5[x], "Odds5");
                }
            }
        }
    }

    void SendToQueues(int number, string oddsName)
    {
        if (number > 0)
        {
            float randomBuffer = Random.Range(0, addQueueBuffer);

            number += (Random.Range(0, 2) == 0) ? 0 : 1;

            var x = Random.Range(0, 2) == 0;

            if (x) StartCoroutine(emailSpawner.AddToQueueWithDelay(number, randomBuffer));
            else StartCoroutine(docSpawner.AddToQueueWithDelay(number, addQueueBuffer - randomBuffer));

            //if (x) Debug.Log(oddsName + " sent " + number + " emails");
            //else   Debug.Log(oddsName + " sent " + number + " docs");
        }
    }
    #endregion

    #region Timer Bar Methods
    private void CalculateBarShrinkFactor() => shrinkFactor = timerBar.transform.localScale.x / gameDurationMax;

    private void UpdateBarColor()
    {
        if (gameDuration <= (gameDurationMax * 2 / 3) && // <= 120 when Max = 180
            gameDuration > (gameDurationMax * 1 / 3) &&  // > 60 when Max = 180
            barSprite.color != warningColor)

        {
            barSprite.color = warningColor;
            clockAnimator.SetTrigger("On");
        }
        else if (gameDuration <= (gameDurationMax * 1 / 3) &&  // <= 60 when Max = 180
                 barSprite.color != shortTimeColor)
        {
            barSprite.color = shortTimeColor;
            clockAnimator.SetTrigger("On");
        }
    }

    private void UpdateBarLength() => timerBar.transform.localScale = new Vector3((shrinkFactor * gameDuration),
                                                                            timerBar.transform.localScale.y,
                                                                            timerBar.transform.localScale.z);
    #endregion
}