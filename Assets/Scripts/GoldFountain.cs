using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFountain : MonoBehaviour
{
	public float rate;
	public float shootForce;

	public Vector2 direction;

	private float count;
	public float max;

	public Transform shootPoint;

	public GameObject LoadingZone;
	public GameObject goldPiece;
	public GameObject goldLump;
	public GameObject goldPile;
	public GameObject goldHill;
	public GameObject goldMountain;

	// Use this for initialization
	void Start ()
	{
		LoadingZone = GameObject.Find("LoadingZone");
		direction = GameObject.Find("DepositHole").transform.position - transform.position;
		rate = 5;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (count < max)
		{
			count += rate * Time.deltaTime;
		}
		if (count >= max)
		{
			if (LoadingZone.transform.childCount > 0)// && (1/Time.deltaTime ) > 20)
			{
				count = 0;
				rate += 0.05f;
				//shootForce = 0;
				GameObject piece = LoadingZone.transform.Find("GoldPiece(Clone)").gameObject;
				if (piece != null)
				{
					//piece.GetComponent<Gold>().enabled = true;
					//piece.GetComponent<Gold>().EnableGold(true);
					piece.SetActive(true);
					piece.GetComponent<Gold>().checkPositionChange = true;
					piece.GetComponent<Gold>().fountain = this;
					piece.transform.position = new Vector3(shootPoint.transform.position.x + Random.Range(-0.1f, 0.1f), shootPoint.transform.position.y + Random.Range(-0.1f, 0.1f), 0);
					piece.transform.SetParent(this.transform);
					
					//GameObject piece = Instantiate(goldPiece, shootPoint.transform.position, shootPoint.rotation);
					//Rigidbody2D rigid = piece.GetComponent<Rigidbody2D>();
					//rigid.AddForce(direction * shootForce);
				}
			}
		}
	}
}
