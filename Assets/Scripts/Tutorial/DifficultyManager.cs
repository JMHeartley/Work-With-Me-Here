using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    #region Difficulty Settings

    Difficulty _easier = new Difficulty(
    "Easy",
    "Slower paced for the causal player",
    "jq-MlSLsSUuKJmpFu0TeiQOkdRTV8rnk6jAdv6my0NUQ",
    "5fa8af86eb371a09c4c51e44",
    new int[][]
    {
            new int[] { 0, 1, 1, 2, 2 }, // first 20 secs
            new int[] { 1, 1, 2, 2, 2 }, // > 150
            new int[] { 1, 2, 2, 3, 3 }, // > 120
            new int[] { 1, 1, 2, 2, 2 }, // > 90
            new int[] { 0, 1, 1, 2, 2 }, // > 60
            new int[] { 0, 0, 1, 1, 2 }, // < 60
            new int[] { 0, 1, 1, 2, 2 } // when disconnected
    });

    Difficulty _easy = new Difficulty(
        "Easy", 
        "Slower paced for the causal player",
        "jq-MlSLsSUuKJmpFu0TeiQOkdRTV8rnk6jAdv6my0NUQ", 
        "5fa8af86eb371a09c4c51e44", 
        new int[][] 
        {
            new int[] { 0, 1, 1, 2, 2 }, // first 20 secs
            new int[] { 1, 1, 2, 2, 2 }, // > 150
            new int[] { 1, 2, 2, 3, 3 }, // > 120
            new int[] { 2, 2, 3, 3, 3 }, // > 90
            new int[] { 1, 2, 2, 3, 4 }, // > 60
            new int[] { 0, 1, 1, 2, 3 }, // < 60
            new int[] { 2, 2, 3, 3, 4 } // when disconnected
        });

    Difficulty _medium = new Difficulty(
        "Medium", 
        "An average day with its ups and downs",
        "Zpsm2zqXA0GmAN6Xuq_zlANEgUkWZzKESVAVtxZivz0g", 
        "5f3714f7eb371809c4cb6f0a", 
        new int[][]
        {
            new int[] { 1, 1, 1, 2, 2 }, // first 20 secs
            new int[] { 1, 1, 2, 2, 3 }, // > 150
            new int[] { 1, 2, 3, 3, 4 }, // > 120
            new int[] { 2, 3, 3, 4, 5 }, // > 90
            new int[] { 1, 2, 3, 4, 4 }, // > 60
            new int[] { 1, 1, 1, 1, 2 }, // < 60
            new int[] { 2, 2, 3, 4, 5 } // when disconnected
        });

    Difficulty _extreme = new Difficulty(
        "Extreme",
        "A challenge for experienced players with strong index fingers!",
        "HY05Hi29VEWwsd_K_0bFPwQe3NRbHJakaA3TnGxBvy-Q",
        "5fa8af78eb371a09c4c51ddc",
        new int[][]
        {
                    new int[] { 1, 1, 1, 2, 2 }, // first 20 secs
                    new int[] { 1, 1, 2, 2, 3 }, // > 150
                    new int[] { 1, 2, 3, 3, 4 }, // > 120
                    new int[] { 2, 3, 4, 5, 6 }, // > 90
                    new int[] { 1, 2, 3, 4, 5 }, // > 60
                    new int[] { 1, 1, 2, 2, 3 }, // < 60
                    new int[] { 2, 3, 3, 3, 3 } // when disconnected
        });
    #endregion

    [SerializeField] Difficulty difficulty;
    [SerializeField] TextMeshProUGUI difficultyMessage;

    private void Awake()
    {
        SceneLoader.NewScene += SetDifficulty;
        Medium();
    }

    private void OnDestroy()
    {
        SceneLoader.NewScene -= SetDifficulty;
    }

    #region Button Methods
    public void Easy()
    {
        difficulty = _easier;
        difficultyMessage.text = _easy.Message;
    }
    public void Medium()
    {
        difficulty = _medium;
        difficultyMessage.text = _medium.Message;
    }
    public void Extreme()
    {
        difficulty = _extreme;
        difficultyMessage.text = _extreme.Message;
    }
    #endregion

    void SetDifficulty()
    {
        FindObjectOfType<GameSession>().SessionDifficulty = difficulty;

        Debug.Log("Difficulty Set: " + difficulty.Name);
    }
}
public class Difficulty
{
    int _perSecs0 = 2;
    int _perSecs1 = 2;
    int _perSecs2 = 2;
    int _perSecs3 = 1;
    int _perSecs4 = 1;
    int _perSecs5 = 1;
    int _perSecsDisconn = 1;

    int[] _odds0; // first 20 secs
    int[] _odds1; // > 150
    int[] _odds2; // > 120
    int[] _odds3; // > 90
    int[] _odds4; // > 60
    int[] _odds5; // < 60
    int[] _oddsDisconn; // when disconnected

    public string Name { get; private set; }
    public string Message { get; private set; }
    public string PrivateCode { get; private set; }
    public string PublicCode { get; private set; }
    public int[] Rates
    {
        get
        {
            return new int[] {_perSecs0, _perSecs1, _perSecs2,
                              _perSecs3, _perSecs4, _perSecs5,
                              _perSecsDisconn};
        }
        private set
        {
            _perSecs0 = value[0];
            _perSecs1 = value[1];
            _perSecs2 = value[2];
            _perSecs3 = value[3];
            _perSecs4 = value[4];
            _perSecs5 = value[5];
            _perSecsDisconn = value[6];
        }
    }
    public int[][] Odds
    {
        get
        {
            return new int[][] { _odds0, _odds1, _odds2, _odds3, _odds4, _odds5, _oddsDisconn };
        }
        private set
        {
            _odds0 = new int[] { value[0][0], value[0][1], value[0][2], value[0][3], value[0][4] };
            _odds1 = new int[] { value[1][0], value[1][1], value[1][2], value[1][3], value[1][4] };
            _odds2 = new int[] { value[2][0], value[2][1], value[2][2], value[2][3], value[2][4] };
            _odds3 = new int[] { value[3][0], value[3][1], value[3][2], value[3][3], value[3][4] };
            _odds4 = new int[] { value[4][0], value[4][1], value[4][2], value[4][3], value[4][4] };
            _odds5 = new int[] { value[5][0], value[5][1], value[5][2], value[5][3], value[5][4] };
            _oddsDisconn = new int[] { value[6][0], value[6][1], value[6][2], value[6][3], value[6][4] };
        }
    }

    public Difficulty(string name, string message, string privateCode, string publicCode, int[][] Odds)
    {
        Name = name;
        Message = message;
        PrivateCode = privateCode;
        PublicCode = publicCode;
        this.Odds = Odds;
    }
}