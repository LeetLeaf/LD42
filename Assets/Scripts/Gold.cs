using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gold : MonoBehaviour
{
	//public int amount;

	public bool master;
	public bool locked;
	public ArrayList goldBros;

	public GoldType type;
	public GameObject[] goldList;

	//private Collider2D triggerBox;
	private CircleCollider2D circleCollider;
	private Rigidbody2D rigi2D;
	private SpriteRenderer spriteRend;

	public GameObject LoadingZone;

	private Vector3 startPosition;
	public bool checkPositionChange;

	public GoldFountain fountain;
	public bool spawned = false;
	public bool coolDown = false;
	public float coolDownTime = 5.0f;
	public float currentCoolDownTime;

	public Vector3 lastPosition;

	public AudioSource audioSource;
	// Use this for initialization
	void Start ()
	{
		//locked = false;
		//master = false;

		//triggerBox = gameObject.GetComponent<BoxCollider2D>();
		circleCollider = gameObject.GetComponent<CircleCollider2D>();
		rigi2D = gameObject.GetComponent<Rigidbody2D>();
		//Physics2D.IgnoreCollision(triggerBox, circleCollider);
		startPosition = this.transform.position;
		fountain = this.transform.parent.gameObject.GetComponent<GoldFountain>();
		LoadingZone = fountain.LoadingZone;
		spriteRend = gameObject.GetComponent<SpriteRenderer>();
		audioSource = GameObject.Find("CoinSound").GetComponent<AudioSource>();

		EnableGold(true);
		EnableRigiBody(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		lastPosition = transform.position;
		if (checkPositionChange && transform.parent.Equals(fountain.transform))
		{
			EnableGold(true);
			EnableRigiBody(true);
			rigi2D.AddForce(fountain.direction * fountain.shootForce);
			spawned = true;
		}
		if (coolDown)
		{
			currentCoolDownTime -= Time.deltaTime;
			if (currentCoolDownTime <= 0)
			{
				coolDown = false;
			}
		}

		if (transform.parent != null && transform.parent.tag == "Fountain")
		{

		}

		if (rigi2D.velocity.magnitude < 0.5f)
		{
			rigi2D.velocity = Vector2.zero;
			rigi2D.Sleep();
		}
		//if (type != GoldType.MOUNTAIN && spawned)
		//{
		//	goldBros = FindGoldInRange();


		//	locked = false;
		//	//goldBros = goldBros(g => g != null);
		//	int sumOfGold = 0;
		//	for (int i = 0; i < goldBros.Count; i++)
		//	{
		//		GameObject g = goldBros[i] as GameObject;
		//		if (g == null)
		//		{
		//			goldBros.RemoveAt(i);
		//			i--;
		//		}
		//		else
		//		{
		//			sumOfGold += (int)g.GetComponent<Gold>().type;
		//		}
		//	}
		//	if (sumOfGold >= (int)GetNextGoldType(type))
		//	{
		//		type = GetNextGoldType(type);
		//		master = false;
		//		if (type == GoldType.LUMP)
		//		{
		//			GameObject piece = LoadingZone.transform.Find("GoldLump(Clone)").gameObject;
		//			piece.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, 0);
		//			piece.transform.SetParent(fountain.transform);
		//			ReturnToLoadingZone();
		//		}
		//		if (type == GoldType.PILE)
		//		{
		//			GameObject piece = LoadingZone.transform.Find("GoldPile(Clone)").gameObject;
		//			piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		//			piece.transform.SetParent(fountain.transform);
		//			ReturnToLoadingZone();
		//		}
		//		if (type == GoldType.HILL)
		//		{
		//			GameObject piece = LoadingZone.transform.Find("GoldHill(Clone)").gameObject;
		//			piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		//			piece.transform.SetParent(fountain.transform);
		//			ReturnToLoadingZone();
		//		}
		//		if (type == GoldType.MOUNTAIN)
		//		{
		//			GameObject piece = LoadingZone.transform.Find("GoldMountain(Clone)").gameObject;
		//			piece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
		//			piece.transform.SetParent(fountain.transform);
		//			ReturnToLoadingZone();
		//		}
		//		foreach (GameObject bro in goldBros)
		//		{
		//			bro.GetComponent<Gold>().ReturnToLoadingZone();
		//		}
		//		goldBros.Clear();
		//	}
			
		//}
	}

	private static GoldType GetNextGoldType(GoldType currentType)
	{
		GoldType retVal = GoldType.PIECE;

		if (currentType == GoldType.PIECE)
		{
			retVal = GoldType.LUMP;
		}
		else if (currentType == GoldType.LUMP)
		{
			retVal = GoldType.PILE;
		}
		else if (currentType == GoldType.PILE)
		{
			retVal = GoldType.HILL;
		}
		else if (currentType == GoldType.HILL)
		{
			retVal = GoldType.MOUNTAIN;
		}
		else if (currentType == GoldType.MOUNTAIN)
		{
			retVal = GoldType.MOUNTAIN; // Max Piece
		}


		return retVal;
	}

	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//	if (!collision.gameObject.Equals(this.gameObject))
	//	{
	//		if (type != GoldType.MOUNTAIN)
	//		{
	//			if (collision.gameObject.tag.Equals("Gold"))
	//			{
	//				if (collision.gameObject.GetComponent<Gold>().type <= type
	//					//&& !collision.gameObject.GetComponent<Gold>().locked
	//					&& !collision.gameObject.GetComponent<Gold>().master)
	//				{
	//					if (!master)
	//					{
	//						master = true;
	//						goldBros = new ArrayList();
	//					}
	//					if (master && !collision.gameObject.GetComponent<Gold>().locked)
	//					{

	//						goldBros.Add(collision.gameObject);
	//						//collision.gameObject.GetComponent<Gold>().locked = true;
	//						collision.gameObject.GetComponent<Gold>().master = false;
	//						Physics2D.IgnoreCollision(collision, triggerBox);
	//					}
	//				}
	//			}
	//		}
	//	}
	//}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Equals("Chest") && !collision.gameObject.GetComponent<Box>().closed)
		{
			ReturnToLoadingZone();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Gold"))
		{
			if (rigi2D.IsSleeping())
			{
				//rigi2D.velocity.Set(rigi2D.velocity.x * 2.0f, rigi2D.velocity.y * 2.0f);
			}
			else
			{
				rigi2D.velocity.Set(rigi2D.velocity.x * 1.2f, rigi2D.velocity.y * 1.2f);
			}
		}
		//if (!coolDown)
		//{
		//	if (collision.gameObject.tag.Equals("Gold") && spawned)
		//	{

			//		if (this.transform.parent == null || this.transform.parent.tag.Equals("Fountain"))
			//		{
			//			collision.gameObject.transform.SetParent(this.transform, true);
			//			for (int i = transform.childCount - 1; i > 0; i--)
			//			{
			//				Physics2D.IgnoreCollision(collision.collider, transform.GetChild(i).GetComponent<Collider2D>(), true);
			//			}
			//		}
			//		else if (!this.transform.parent.tag.Equals("Fountain"))
			//		{
			//			collision.gameObject.transform.SetParent(this.transform.parent.transform, true);
			//			for (int i = transform.parent.childCount - 1; i > 0; i--)
			//			{
			//				Physics2D.IgnoreCollision(collision.collider, transform.parent.GetChild(i).GetComponent<Collider2D>(), true);
			//			}
			//		}

			//		EnableRigiBody(false);

			//		Physics2D.IgnoreCollision(collision.collider, boxCollider, true);

			//	}	
			//}
		if (collision.gameObject.tag.Equals("Player"))
		{
			if (rigi2D.IsSleeping())
			{
				rigi2D.WakeUp();
				rigi2D.velocity.Set(rigi2D.velocity.x * 1.2f, rigi2D.velocity.y * 1.2f);
			}
			//if (this.transform.parent != null)
			//{
			//	Physics2D.IgnoreCollision(boxCollider, this.transform.parent.GetComponent<Collider2D>(), false);
			//}
			//for (int i = transform.childCount - 1; i > 0; i--)
			//{
			//	Physics2D.IgnoreCollision(boxCollider, transform.GetChild(i).GetComponent<Collider2D>(), false);
			//}
			//transform.DetachChildren();
			//collision.gameObject.transform.parent = null;
			//coolDown = true;
			//currentCoolDownTime = coolDownTime;
			//EnableRigiBody(true);		
		}

		if (collision.gameObject.tag.Equals("Map"))
		{
			transform.position = lastPosition;
		}
		//if (collision.gameObject.tag.Equals("Map"))
		//{
		//	//if (!rigi2D.IsSleeping())
		//	//{
		//	//	rigi2D.Sleep();
		//	//	//rigi2D.WakeUp();
		//	//}
		//	ReturnToLoadingZone();
		//}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Map"))
		{
			ReturnToLoadingZone(false);
			//transform.position = lastPosition;
		}
	}

	public void EnableGold(bool enableThis)
	{
		circleCollider.enabled = enableThis;
		checkPositionChange = !enableThis;
		//spriteRend.enabled = enableThis;
		gameObject.SetActive(enableThis);
	}

	public void EnableRigiBody(bool enableThis)
	{
		if (!enableThis)
		{
			rigi2D.Sleep();
		}
		else
		{
			rigi2D.WakeUp();
		}
	}

	public void ReturnToLoadingZone(bool playSound = true)
	{
		if (playSound)
		{
			audioSource.PlayOneShot(audioSource.clip);
		}
		this.gameObject.transform.position = LoadingZone.transform.position;
		this.gameObject.transform.SetParent(LoadingZone.transform);
		spawned = false;
		EnableRigiBody(false);
		EnableGold(false);
	}

	public ArrayList FindGoldInRange()
	{
		ArrayList allGold = new ArrayList();
		allGold.AddRange(GameObject.FindGameObjectsWithTag("Gold"));
		ArrayList goldInRange = new ArrayList();
		float distance = 0.4f;
		Vector3 position = transform.position;
		foreach (GameObject go in allGold)
		{
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				goldInRange.Add(go);
			}
		}
		return goldInRange;
	}

}

public enum GoldType
{
	PIECE = 1,
	LUMP = 10,
	PILE = 50,
	HILL = 100,
	MOUNTAIN = 500
}



