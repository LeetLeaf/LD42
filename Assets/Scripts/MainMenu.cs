using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab;

public class MainMenu : MonoBehaviour
{
	public Button leaderboardButton;
	public GameObject loginButton;
	// Use this for initialization
	void Start()
	{
		if (PlayFabClientAPI.IsClientLoggedIn())
		{
			Destroy(loginButton);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ButtonStartClick()
	{
		SceneManager.LoadScene("Game");
	}

	public void LeaderBoardButton()
	{
		if (PlayFabClientAPI.IsClientLoggedIn())
		{
			SceneManager.LoadScene("LeaderBoards");
		}
		else
		{
			SceneManager.LoadScene("Login");
		}
	}

	public void LoginButton()
	{
		SceneManager.LoadScene("Login");
	}
}
