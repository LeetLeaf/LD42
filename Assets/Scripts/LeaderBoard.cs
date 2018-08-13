using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderBoard : MonoBehaviour
{
	public Text outputData;
	public GameObject highScorePopup;

	void Awake()
	{
		GetLeaderboard();
	}	

	public void GetLeaderboard()
	{

		//Debug.Log("Fetching Leaderboard Data...");

		//new GameSparks.Api.Requests.LeaderboardDataRequest()
		//	.SetLeaderboardShortCode("TOP_SCORE")
		//	.SetEntryCount(100) 
		//	.Send((response) => {

		//		if (!response.HasErrors)
		//		{
		//			Debug.Log("Found Leaderboard Data...");
		//			outputData.text = System.String.Empty; 
		//			foreach (GameSparks.Api.Responses.LeaderboardDataResponse._LeaderboardData entry in response.Data) 
		//			{
		//				int rank = (int)entry.Rank; 
		//				string playerName = entry.UserName;
		//				string score = entry.JSONData["SCORE"].ToString(); 
		//				outputData.text += rank + ". " + playerName + ": " + score + "\n"; 
		//			}
		//		}
		//		else
		//		{
		//			Debug.Log("Error Retrieving Leaderboard Data...");
		//		}

		//	});


		List<PlayerLeaderboardEntry> leaderboardList = new List<PlayerLeaderboardEntry>();
		PlayFabClientAPI.GetLeaderboard(
	   new GetLeaderboardRequest()
	   {
		   StatisticName = "score",
		   StartPosition = 0,
		   MaxResultsCount = 100
	   },
	   result =>
	   {
		   outputData.text = System.String.Empty;
		   foreach (PlayerLeaderboardEntry entry in result.Leaderboard)
		   {
			   int rank = entry.Position + 1;
			   string playerName = entry.DisplayName;
			   int score = entry.StatValue;
			   outputData.text += rank + ". " + playerName + ": " + score + "\n";
		   }
	   }
		,
	   error => Debug.Log(error.GenerateErrorReport())
		);

		
	}

	public void MainMenuButton()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
















