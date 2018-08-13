using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class NewGold : MonoBehaviour
{
	private Tilemap tileMap;
	// Use this for initialization
	void Start ()
	{
		tileMap = transform.GetComponent<Tilemap>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

		Vector3 hitPosition = Vector3.zero;
		if (tileMap != null && tileMap.gameObject == collision.gameObject)
		{
			foreach (ContactPoint2D hit in collision.contacts)
			{
				hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
				hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
				tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{

		Vector3 hitPosition = Vector3.zero;
		if (tileMap != null )//&& tileMap.gameObject == collision.gameObject)
		{
			foreach (ContactPoint2D hit in collision.contacts)
			{
				hitPosition.x = hit.point.x - 0.1f * hit.normal.x;
				hitPosition.y = hit.point.y - 0.1f * hit.normal.y;
				tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
			}
		}

	}
}
