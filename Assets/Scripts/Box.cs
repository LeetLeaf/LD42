using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public int maxSize;
	public int currentHoldings;

	public Sprite[] sprites;

	public bool IsHeld;
	public GameObject holder;

	public Vector3 heldOffSet;

	private SpriteRenderer spriteRenderer;
	private BoxCollider2D boxCollider;

	public GameManager gameManager;
	public bool closed;

	public float heldCoolDown;
	public GameObject progressBarFull;
	public GameObject progressBar;

	// Use this for initialization
	void Start ()
	{
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{



		if (currentHoldings >= maxSize)
		{
			spriteRenderer.sprite = sprites[1];
			closed = true;
			Destroy(progressBarFull);
			Destroy(progressBar);
		}
		else
		{
			progressBarFull.transform.localScale = new Vector3((float)currentHoldings / (float)maxSize, 1, 1);
		}
		if (holder != null && IsHeld)
		{
			transform.position = holder.transform.position + heldOffSet;

			if (Input.GetKeyDown(KeyCode.A))
			{
				heldOffSet.x = Mathf.Abs(heldOffSet.x) * -1;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				heldOffSet.x = Mathf.Abs(heldOffSet.x);
			}
		}
		else if (holder == null)
		{
			if (this.gameObject.transform.parent != null)
			{
				holder = this.gameObject.transform.parent.gameObject;
				IsHeld = true;
				heldOffSet = gameObject.transform.position - gameObject.transform.position;
				boxCollider.isTrigger = true;
			}
		}
		
		if (heldCoolDown > 0)
		{
			IsHeld = false;
			heldCoolDown -= Time.deltaTime;
			boxCollider.isTrigger = false;
			transform.parent = null;
		}
		else if ( holder != null && !IsHeld)
		{
			Physics2D.IgnoreCollision(boxCollider, holder.GetComponent<Collider2D>(), false);
			holder = null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(IsHeld)
		{
			if (currentHoldings < maxSize)
			{
				if (collision.gameObject.tag.Equals("Gold"))
				{
					if (currentHoldings <= maxSize)
					{
						currentHoldings++;
					}
					//	Gold gold = collision.gameObject.GetComponent<Gold>();
					//int amount = (int)gold.type;

					//	gold.ReturnToLoadingZone();
					//	gold.enabled = false;
					//}
				}
			}
			else
			{
				if (collision.gameObject.name.Equals("DepositHole"))
				{
					gameManager.RespectPoints += currentHoldings;
					gameManager.Score += currentHoldings;
					Destroy(gameObject);
				}
			}
		}
	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (currentHoldings < maxSize && IsHeld)
	//	{
	//		if (collision.gameObject.tag.Equals("Gold"))
	//		{
	//			int amount = collision.gameObject.GetComponent<Gold>().amount;
	//			if (amount + currentHoldings <= maxSize)
	//			{
	//				currentHoldings += amount;
	//				Destroy(collision.gameObject);
	//			}
	//		}
	//	}
	//}
}
