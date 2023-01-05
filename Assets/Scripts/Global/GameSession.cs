using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [Header("Debugging")]
    [Tooltip("backspace: subtract 30 game secs")] public bool timerDebug;
    [Tooltip("tab: dog happiness zero")] public bool dogDebug;
    [Tooltip("end screen: insert fake scores")] public bool insertFakeScores;
    [Tooltip("")] public bool disablePause;
    [Tooltip("")] public bool disableTooltips;

    /*
    int perSeconds0;
    int perSeconds1;
    int perSeconds2;
    int perSeconds3;
    int perSeconds4;
    int perSeconds5;
    int perSecondsDisconn;

    int[] Odds0; // first 20 secs
    int[] Odds1; // > 150
    int[] Odds2; // > 120
    int[] Odds3; // > 90
    int[] Odds4; // > 60
    int[] Odds5; // < 60
    int[] OddsDisconn; // when disconnected

    public int[] Rates
    {
        get
        {
            return new int[] {perSeconds0, perSeconds1, perSeconds2,
                              perSeconds3, perSeconds4, perSeconds5,
                              perSecondsDisconn};
        }
        set
        {
            perSeconds0 = value[0];
            perSeconds1 = value[1];
            perSeconds2 = value[2];
            perSeconds3 = value[3];
            perSeconds4 = value[4];
            perSeconds5 = value[5];
            perSecondsDisconn = value[6];
        }
    }
    public int[][] Odds
    {
        get
        {
            return new int[][] { Odds0, Odds1, Odds2, Odds3, Odds4, Odds5, OddsDisconn };
        }
        set
        {
            Odds0 = new int[] { value[0][0], value[0][1], value[0][2], value[0][3], value[0][4] };
            Odds1 = new int[] { value[1][0], value[1][1], value[1][2], value[1][3], value[1][4] };
            Odds2 = new int[] { value[2][0], value[2][1], value[2][2], value[2][3], value[2][4] };
            Odds3 = new int[] { value[3][0], value[3][1], value[3][2], value[3][3], value[3][4] };
            Odds4 = new int[] { value[4][0], value[4][1], value[4][2], value[4][3], value[4][4] };
            Odds5 = new int[] { value[5][0], value[5][1], value[5][2], value[5][3], value[5][4] };
            OddsDisconn = new int[] { value[6][0], value[6][1], value[6][2], value[6][3], value[6][4] };
        }
    }
    public string LeaderboardPublicCode { get; set; }
    public string LeaderboardPrivateCode { get; set; }
    public string DifficultyName { get; set; }
    */

    static public bool IsFirstGame { get; private set; } = true;
    public Difficulty SessionDifficulty { get; set; }
    public static bool MobileSession { get; set; }

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
            Debug.Log("GameSession in this room was destroyed");
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        Timer.GameOver += UpdateBool;
    }

    private void OnDestroy()
    {
        Timer.GameOver += UpdateBool;
    }

    private void Start()
    {
        if (timerDebug) Debug.Log("Time debug enabled: backspace");
        if (dogDebug) Debug.Log("Dog debug enabled: tab");
        if (insertFakeScores) Debug.Log("Stats debug enabled: Insert fakes");
        if (disablePause) Debug.Log("Auto pause disabled");

        //Debug.Log("isFirst: " + IsFirstGame, gameObject);
    }

    void UpdateBool() => IsFirstGame = false;
}