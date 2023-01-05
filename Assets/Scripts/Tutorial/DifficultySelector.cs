 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] Difficulty sessionDifficulty = Difficulty.Easy;
    [SerializeField] TextMeshProUGUI difficultyMessage;

    #region Easy Settings
    int perSecsEasy0 = 2;
    int perSecsEasy1 = 2;
    int perSecsEasy2 = 2;
    int perSecsEasy3 = 1;
    int perSecsEasy4 = 1;
    int perSecsEasy5 = 1;
    int perSecsEasyDisconn = 1;

    int[] easy0 = { 0, 1, 2, 2, 2 }; // first 20 secs
    int[] easy1 = { 1, 1, 2, 2, 2 }; // > 150
    int[] easy2 = { 1, 2, 2, 3, 3 }; // > 120
    int[] easy3 = { 2, 2, 3, 3, 3 }; // > 90
    int[] easy4 = { 0, 2, 3, 3, 4 }; // > 60
    int[] easy5 = { 0, 1, 2, 2, 3 }; // < 60
    int[] easyDisconn = { 2, 2, 3, 3, 4 }; // when disconnected

    string _easyPrivateCode = "jq-MlSLsSUuKJmpFu0TeiQOkdRTV8rnk6jAdv6my0NUQ";
    string _easyPublicCode = "5fa8af86eb371a09c4c51e44";

    #endregion

    #region Medium Settings
    int perSecsMedium0 = 2;
    int perSecsMedium1 = 2;
    int perSecsMedium2 = 2;
    int perSecsMedium3 = 1;
    int perSecsMedium4 = 1;
    int perSecsMedium5 = 1;
    int perSecsMediumDisconn = 1;

    int[] medium0 = { 1, 1, 1, 2, 2 }; // first 20 secs         
    int[] medium1 = { 1, 1, 2, 2, 3 }; // > 150
    int[] medium2 = { 1, 2, 3, 3, 4 }; // > 120
    int[] medium3 = { 2, 3, 3, 4, 5 }; // > 90
    int[] medium4 = { 1, 2, 3, 4, 4 }; // > 60
    int[] medium5 = { 1, 1, 1, 1, 2 }; // < 60
    int[] mediumDisconn = { 2, 2, 3, 4, 5 }; // when disconnected

    string _mediumPrivateCode = "Zpsm2zqXA0GmAN6Xuq_zlANEgUkWZzKESVAVtxZivz0g";
    string _mediumPublicCode = "5f3714f7eb371809c4cb6f0a";
    #endregion

    #region Hard Settings
    int perSecsHard0 = 2;
    int perSecsHard1 = 2;
    int perSecsHard2 = 2;
    int perSecsHard3 = 1;
    int perSecsHard4 = 1;
    int perSecsHard5 = 1;
    int perSecsHardDisconn = 1;

    int[] hard0 = { 0, 2, 2, 2, 2 }; // first 20 secs
    int[] hard1 = { 2, 2, 2, 2, 2 }; // > 150
    int[] hard2 = { 2, 2, 2, 2, 4 }; // > 120
    int[] hard3 = { 2, 2, 3, 3, 3 }; // > 90
    int[] hard4 = { 2, 2, 3, 4, 4 }; // > 60
    int[] hard5 = { 0, 2, 2, 2, 4 }; // < 60
    int[] hardDisconn = { 2, 2, 2, 3, 4 }; // when disconnected

    string _hardPrivateCode;
    string _hardPublicCode;
    #endregion

    #region Extreme Settings
    int perSecsExtreme0 = 2;
    int perSecsExtreme1 = 2;
    int perSecsExtreme2 = 2;
    int perSecsExtreme3 = 1;
    int perSecsExtreme4 = 1;
    int perSecsExtreme5 = 1;
    int perSecsExtremeDisconn = 1;

    int[] extreme0 = { 1, 1, 1, 2, 2 }; // first 20 secs
    int[] extreme1 = { 1, 1, 2, 2, 3 }; // > 150
    int[] extreme2 = { 1, 2, 3, 3, 4 }; // > 120
    int[] extreme3 = { 2, 3, 4, 5, 6 }; // > 90
    int[] extreme4 = { 1, 2, 3, 4, 5 }; // > 60
    int[] extreme5 = { 1, 1, 2, 2, 3 }; // < 60
    int[] extremeDisconn = { 2, 3, 3, 3, 3 }; // when disconnected

    string _extremePrivateCode = "HY05Hi29VEWwsd_K_0bFPwQe3NRbHJakaA3TnGxBvy-Q";
    string _extremePublicCode = "5fa8af78eb371a09c4c51ddc";
    #endregion

    #region Prefect recovery
    /*
    int[] medium0 = { 0, 1, 2, 2, 2 }; // first 20 secs             0.7 = ( 7/5 or 1.4) / 2
    int[] medium1 = { 1, 1, 2, 2, 2 }; // > 150                     0.8 = ( 8/5 or 1.6) / 2
    int[] medium2 = { 1, 2, 2, 3, 3 }; // > 120                     1.1 = (11/5 or 2.2) / 2
    int[] medium3 = { 2, 2, 3, 3, 3 }; // > 90                      2.6 = (13/5 or 2.6) / 1
    int[] medium4 = { 0, 2, 3, 3, 4 }; // > 60                      2.4 = (12/5 or 2.4) / 1
    int[] medium5 = { 0, 1, 2, 2, 3 }; // < 60                      1.6 = ( 8/5 or 1.6) / 1
    int[] mediumDisconn = { 0, 2, 3, 3, 4 }; // when disconnected   2.4 = (12/5 or 2.4) / 1
    */
    #endregion

    GameSession gameSession;

    enum Difficulty
    {
        Easy, Medium, Hard, Extreme
    }

    void Awake()
    {
        SceneLoader.NewScene += SetOdds;

        gameSession = FindObjectOfType<GameSession>();

        Easy();
    }

    void OnDestroy()
    {
        SceneLoader.NewScene -= SetOdds;
    }

    #region Button Methods
    public void Easy()
    {
        sessionDifficulty = Difficulty.Easy;
        difficultyMessage.text = "Slower paced for the causal player";
    }

    public void Medium()
    {
        sessionDifficulty = Difficulty.Medium;
        difficultyMessage.text = "An average day with its ups and downs";
    }

    public void Hard()
    {
        sessionDifficulty = Difficulty.Hard;
        difficultyMessage.text = "Hard";
    }

    public void Extreme()
    {
        sessionDifficulty = Difficulty.Extreme;
        difficultyMessage.text = "A challenge for experienced players with strong index fingers!";
    }
    #endregion

    void SetOdds()
    {
        /*
        switch (sessionDifficulty)
        {
            case Difficulty.Easy:
                gameSession.Odds = new int[][] { easy0, easy1, easy2, easy3, easy4, easy5, easyDisconn };
                gameSession.Rates = new int[] { perSecsEasy0, perSecsEasy1, perSecsEasy2,
                                                perSecsEasy3, perSecsEasy4, perSecsEasy5, perSecsEasyDisconn };
                gameSession.LeaderboardPrivateCode = _easyPrivateCode;
                gameSession.LeaderboardPublicCode = _easyPublicCode;
                break;
            case Difficulty.Medium:
                gameSession.Odds = new int[][] { medium0, medium1, medium2, medium3, medium4, medium5, mediumDisconn };
                gameSession.Rates = new int[] { perSecsMedium0, perSecsMedium1, perSecsMedium2,
                                                perSecsMedium3, perSecsMedium4, perSecsMedium5, perSecsMediumDisconn };
                gameSession.LeaderboardPrivateCode = _mediumPrivateCode;
                gameSession.LeaderboardPublicCode = _mediumPublicCode;
                break;
            case Difficulty.Hard:
                gameSession.Odds = new int[][] { hard0, hard1, hard2, hard3, hard4, hard5, hardDisconn };
                gameSession.Rates = new int[] { perSecsHard0, perSecsHard1, perSecsHard2,
                                                perSecsHard3, perSecsHard4, perSecsHard5, perSecsHardDisconn };
                gameSession.LeaderboardPrivateCode = _hardPrivateCode;
                gameSession.LeaderboardPublicCode = _hardPublicCode;
                break;
            case Difficulty.Extreme:
                gameSession.Odds = new int[][] { extreme0, extreme1, extreme2, extreme3, extreme4, extreme5, extremeDisconn };
                gameSession.Rates = new int[] { perSecsExtreme0, perSecsExtreme1, perSecsExtreme2,
                                                perSecsExtreme3, perSecsExtreme4, perSecsExtreme5, perSecsExtremeDisconn };
                gameSession.LeaderboardPrivateCode = _extremePrivateCode;
                gameSession.LeaderboardPublicCode = _extremePublicCode;
                break;
        }

        gameSession.DifficultyName = sessionDifficulty.ToString();

        Debug.Log("Odds set: " + gameSession.DifficultyName);
        */
    }
}

