using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
	public GameObject[] ObjectsToLoad;
	public int LoadHowMany;
	// Use this for initialization
	void Start ()
	{
		//fountain = GameObject.Find("GoldFountain").GetComponent<GoldFountain>();
		foreach (GameObject g in ObjectsToLoad)
		{
			for (int i = 0; i < LoadHowMany; i++)
			{
				GameObject temp = Instantiate(g, this.transform);
				temp.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
