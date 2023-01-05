using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using static dreamloLeaderBoard;
using System.Threading.Tasks;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] Color playerColor;
    [SerializeField] Transform scoreEntryTemplate;
    [SerializeField] TextMeshProUGUI difficultyDisplay;

    Transform scoresContainer;
    dreamloLeaderBoard dreamlo;

    private void Awake()
    {
        dreamlo = GetSceneDreamloLeaderboard();

        scoresContainer = scoreEntryTemplate.parent;
        scoreEntryTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(Display(1, 100));
        difficultyDisplay.text += FindObjectOfType<GameSession>().SessionDifficulty.Name;
    }

    IEnumerator Display(int upperBound, int  lowerBound)
    {
        StartCoroutine(dreamlo.GetScores(upperBound - 1, lowerBound + upperBound - 1));

        while (dreamlo.HasEmptyScores)
        {
            yield return null;
        }

        StartCoroutine(RefreshScreen(upperBound));
    }

    private IEnumerator RefreshScreen(int startingRank)
    {
        int cachedRank = 0;

        var currentRank = startingRank;

        foreach (var item in dreamlo.ToListHighToLow())
        {
            var scoreEntry = Instantiate(scoreEntryTemplate, scoresContainer);

            scoreEntry.gameObject.SetActive(true);

            scoreEntry.GetComponent<ScoreEntry>().SetFields(currentRank, item.shortText);

            if (item.playerName == SubmitName.TimeStamp)
            {
                scoreEntry.GetComponent<ScoreEntry>().SetColor(playerColor);

                Debug.Log("Current player", scoreEntry.gameObject);

                cachedRank = currentRank;
            }

            currentRank++;
        }

        yield return new WaitForSeconds(0.1f);

        scoresContainer.transform.localPosition = new Vector3(scoresContainer.transform.localPosition.x,
                                                            (cachedRank - 1) * 80,
                                                            scoresContainer.transform.localPosition.z);
    }
}