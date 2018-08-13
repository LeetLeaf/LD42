using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class SubmitScore : MonoBehaviour
{
	public Button submitButton;

	public GameObject highScorePopup;

	void Awake()
	{
		//GameSparks.Api.Messages.NewHighScoreMessage.Listener += HighScoreMessageHandler; // assign the New High Score message
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void PostScoreBttn()
	{
		//Debug.Log("Posting Score To Leaderboard...");
		//new GameSparks.Api.Requests.LogEventRequest()
		//	.SetEventKey("SUBMIT_SCORE")
		//	.SetEventAttribute("SCORE", GameObject.Find("GameManager").GetComponent<GameManager>().Score)
		//	.Send((response) => {

		//		if (!response.HasErrors)
		//		{
		//			Debug.Log("Score Posted Sucessfully...");
		//			submitButton.interactable = false;
		//		}
		//		else
		//		{
		//			Debug.Log("Error Posting Score...");
		//		}
		//	});

		PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
		{
			// request.Statistics is a list, so multiple StatisticUpdate objects can be defined if required.
			Statistics = new List<StatisticUpdate> {
		new StatisticUpdate { StatisticName = "score", Value = GameObject.Find("GameManager").GetComponent<GameManager>().Score },
		}
		},
		result => { Debug.Log("User statistics updated"); submitButton.interactable = false; },
		error => { Debug.LogError(error.GenerateErrorReport()); });

	}
}
