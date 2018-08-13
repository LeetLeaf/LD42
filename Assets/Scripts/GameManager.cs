using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public int Score;
	public int RespectPoints;

	public GameObject shopCanvas;
	public GameObject shopSpawnPoint;
	private GameObject panel1;
	private GameObject panel2;

	public Text scoreText;

	public GameObject box;
	public GameObject chest;
	public GameObject Peaset;

	public GameObject Knight;
	public GameObject Paladin;
	public GameObject DragonSlayer;

	public static GameManager instance;

	private AudioSource audioSource;
	public AudioClip buyClip;
	public AudioClip cantBuyClip;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		shopCanvas = GameObject.Find("ShopUICanvas");
		panel1 = shopCanvas.transform.Find("Panel1").gameObject;
		panel2 = shopCanvas.transform.Find("Panel2").gameObject;
		shopSpawnPoint = GameObject.Find("ShopSpawnPoint");
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = Score + "\n"+ RespectPoints;
	}

	public void RightButtonClicked()
	{
		panel1.SetActive(false);
		panel2.SetActive(true);
	}

	public void LeftButtonClicked()
	{
		panel1.SetActive(true);
		panel2.SetActive(false);
	}

	public void BoxButtonClicked()
	{
		if (RespectPoints - 25 >= 0)
		{
			Instantiate(box, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 25;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}
	public void ChestButtonClicked()
	{
		if (RespectPoints - 50 >= 0)
		{
			Instantiate(chest, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 50;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}


	public void PeasetButtonClicked()
	{
		if (RespectPoints - 500 >= 0)
		{
			Instantiate(Peaset, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 500;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}

	public void KnightButtonClicked()
	{
		if (RespectPoints - 1000 >= 0)
		{
			Instantiate(Knight, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 1000;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}
	public void PaladinButtonClicked()
	{
		if (RespectPoints - 2000 >= 0)
		{
			Instantiate(Paladin, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 2000;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}
	public void DragonSlayerButtonClicked()
	{
		if (RespectPoints - 3000 >= 0)
		{
			Instantiate(DragonSlayer, shopSpawnPoint.transform.position, shopSpawnPoint.transform.rotation);
			RespectPoints -= 3000;
			audioSource.PlayOneShot(buyClip);
		}
		else
		{
			audioSource.PlayOneShot(cantBuyClip);
		}
	}
}
