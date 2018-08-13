using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
	public GameManager gameManager;

	public Text uiText;

	public Button submitScore;
	// Use this for initialization
	void Start ()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		uiText.text = "Score: " + gameManager.Score;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void MainMenuClick()
	{
		Destroy(gameManager.gameObject);
		SceneManager.LoadScene("MainMenu");
	}
}
