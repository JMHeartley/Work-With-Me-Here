using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine.Networking;

public class dreamloLeaderBoard : MonoBehaviour {

	string dreamloWebserviceURL = "https://www.dreamlo.com/lb/";

	string _privateCode = "";
	string _publicCode = "";
	
	string highScores = "";

    public bool HasEmptyScores => string.IsNullOrWhiteSpace(highScores);

    private void Awake()
    {
		var difficulty = FindObjectOfType<GameSession>().SessionDifficulty;

		_privateCode = difficulty.PrivateCode;
		_publicCode = difficulty.PublicCode;
    }

    public struct Score {
		public string playerName;
		public int score;
		public int seconds;
		public string shortText;
		public string dateString;
	}
	
	public static dreamloLeaderBoard GetSceneDreamloLeaderboard()
	{
		var go = GameObject.Find("dreamloPrefab");
		
		if (go == null) 
		{
			Debug.LogError("Could not find dreamloPrefab in the scene.");
			return null;
		}

		return go.GetComponent<dreamloLeaderBoard>();
	}

    #region Unused Stock Code
    #region AddScore Methods

    public void AddScore(string playerName, int totalScore)
	{
		AddScoreWithPipe(playerName, totalScore);
	}
	
	public void AddScore(string playerName, int totalScore, int totalSeconds)
	{
		AddScoreWithPipe(playerName, totalScore, totalSeconds);
	}
	
	public void AddScore(string playerName, int totalScore, int totalSeconds, string shortText)
	{
		AddScoreWithPipe(playerName, totalScore, totalSeconds, shortText);
	}
	#endregion

	#region AddScoreWithPipe

	// This function saves a trip to the server. Adds the score and retrieves results in one trip.
	void AddScoreWithPipe(string playerName, int totalScore)
	{
		playerName = Clean(playerName);
		StartCoroutine(GetRequest(dreamloWebserviceURL + _privateCode + "/add-pipe/" + UnityWebRequest.EscapeURL(playerName) + "/" + totalScore.ToString()));
	}
	
	void AddScoreWithPipe(string playerName, int totalScore, int totalSeconds)
	{
		playerName = Clean(playerName);
		StartCoroutine(GetRequest(dreamloWebserviceURL + _privateCode + "/add-pipe/" + UnityWebRequest.EscapeURL(playerName) + "/" + totalScore.ToString()+ "/" + totalSeconds.ToString()));
	}
	
	void AddScoreWithPipe(string playerName, int totalScore, int totalSeconds, string shortText)
	{
		playerName = Clean(playerName);
		shortText = Clean(shortText);
		
		StartCoroutine(GetRequest(dreamloWebserviceURL + _privateCode + "/add-pipe/" + UnityWebRequest.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString()+ "/" + shortText));
	}
    #endregion

    public void GetScores()
	{
		highScores = "";
		StartCoroutine(GetRequest(dreamloWebserviceURL +  _publicCode  + "/pipe"));
	}
	
	void GetSingleScore(string playerName)
	{
		highScores = "";
		StartCoroutine(GetRequest(dreamloWebserviceURL +  _publicCode  + "/pipe-get/" + UnityWebRequest.EscapeURL(playerName)));
	}

	IEnumerator GetRequest(string url)
	{
		// Something not working? Try copying/pasting the url into your web browser and see if it works.
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			yield return www.SendWebRequest();

			//error handling here
			
			highScores = www.downloadHandler.text;
		}

		Debug.Log(url);

		Debug.Log("Got highscores!");
	}
    #endregion

    #region My Code
    public IEnumerator AddScoreRetrieveCouroutine(string playerName, int totalScore, int totalSeconds, string shortText)
    {
        playerName = Clean(playerName);
        shortText = Clean(shortText);

        highScores = "";
		Debug.Log("Highscores were emptied!");

        var url = dreamloWebserviceURL + _privateCode + "/add-pipe/" + UnityWebRequest.EscapeURL(playerName) + "/" + totalScore.ToString() + "/" + totalSeconds.ToString()+ "/" + shortText;

        // Something not working? Try copying/pasting the url into your web browser and see if it works.
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            //error handling here

            highScores = www.downloadHandler.text;
        }
		Debug.Log("Highscores retrieved!");
    }

    public IEnumerator GetScores(int upperBound, int lowerBound)
    {
        highScores = "";
        var url = dreamloWebserviceURL + _publicCode + "/pipe/" + upperBound + lowerBound;

        // Something not working? Try copying/pasting the url into your web browser and see if it works.
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            highScores = www.downloadHandler.text;
        }
    }

    #endregion

    #region Formatting Methods
    public string[] ToStringArray()
	{
		if (this.highScores == null) return null;
		if (this.highScores == "") return null;
		
		var rows = this.highScores.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		return rows;
	}
	
	public List<Score> ToListLowToHigh()
	{
		var scoreList = this.ToScoreArray();
		
		if (scoreList == null) return new List<Score>();
		
		var genericList = new List<Score>(scoreList);
			
		genericList.Sort((x, y) => x.score.CompareTo(y.score));
		
		return genericList;
	}
	
	public List<Score> ToListHighToLow()
	{
		var scoreList = this.ToScoreArray();
		
		if (scoreList == null) return new List<Score>();

		List<Score> genericList = new List<Score>(scoreList);
			
		genericList.Sort((x, y) => y.score.CompareTo(x.score));
		
		return genericList;
	}
	
	public Score[] ToScoreArray()
	{
		var rows = ToStringArray();
		if (rows == null) return null;
		
		int rowcount = rows.Length;
		
		if (rowcount <= 0) return null;
		
		var scoreList = new Score[rowcount];
		
		for (int i = 0; i < rowcount; i++)
		{
			var values = rows[i].Split(new char[] {'|'}, System.StringSplitOptions.None);
			
			var current = new Score();
			current.playerName = values[0];
			current.score = 0;
			current.seconds = 0;
			current.shortText = "";
			current.dateString = "";
			if (values.Length > 1) current.score = CheckInt(values[1]);
			if (values.Length > 2) current.seconds = CheckInt(values[2]);
			if (values.Length > 3) current.shortText = values[3];
			if (values.Length > 4) current.dateString = values[4];
			scoreList[i] = current;
		}
		
		return scoreList;
	}
	
	
	// Keep pipe and slash out of names
	
	string Clean(string s)
	{
		s = s.Replace("/", "");
		s = s.Replace("|", "");
		return s;
		
	}
	
	int CheckInt(string s)
	{
		int x = 0;
		
		int.TryParse(s, out x);
		return x;
	}
    #endregion
}