using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopKeeper : MonoBehaviour
{
	public int maxGoldCount;
	public int currentGoldCount;

	public float deathStartTime;
	public float deathTime;
	public bool deathStart;

	private int nextTick;
	private int lastTick;

	public float goldRange;

	public SpriteRenderer deathTimerRender;
	public Sprite[] deathTimeSprites;

	private AudioSource audioSource;
	// Use this for initialization
	void Start ()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentGoldCount = FindGoldInRange();
		if (currentGoldCount > maxGoldCount)
		{
			deathStart = true;
		}
		else
		{
			deathTime = deathStartTime;
			deathStart = false;
		}
		if (deathStart)
		{
			nextTick = (int)deathTime;
			if (nextTick != lastTick)
			{
				if (!audioSource.isPlaying)
				{
					audioSource.PlayOneShot(audioSource.clip);
				}
			}
			lastTick = nextTick;

			deathTime -= Time.deltaTime;
		}
		if (deathTime < 0)
		{
			SceneManager.LoadScene("GameOver");
			Destroy(gameObject);
		}
		if (deathStart)
		{
			deathTimerRender.enabled = true;
			deathTimerRender.sprite = deathTimeSprites[(int)deathTime];
		}
		else
		{
			//audioSource.Stop();
			deathTimerRender.enabled = false;
		}
	}

	public int FindGoldInRange()
	{
		ArrayList allGold = new ArrayList();
		allGold.AddRange(GameObject.FindGameObjectsWithTag("Gold"));
		int goldCount = 0;
		float distance = goldRange;
		Vector3 position = transform.position;
		foreach (GameObject go in allGold)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				goldCount++;
			}
		}
		return goldCount;
	}
}
