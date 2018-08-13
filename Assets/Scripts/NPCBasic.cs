using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
	public Sprite[] spriteArray;
	public NPCState currentState;

	private float animationTime;
	private SpriteRenderer spriteRenderer;

	public int currentFrame;
	public float walkSpeed;
	public float runSpeed;
	public float chargeSpeed;
	public float collectSpeed;
	public float randomTimeChange;
	public Vector3 currentTarget;

	private GameObject player;

	private float chargeTime;

	public GameObject chargeArrow;
	private SpriteRenderer chargeArrowRenderer;

	public GameObject heldChest;

	private int previousGoldCount;

	private GameObject depositHole;
	public int maxChestDeposits;
	public int currentChestDepostits;

	public Vector3 lastPosition;
	// Use this for initialization
	void Start ()
	{
		if (currentState == NPCState.IDLE)
		{
			currentState = NPCState.WALKING;
		}
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player");
		if (transform.name != "Shopkeeper")
		{
			chargeArrow = transform.Find("AttackArrow").gameObject;
			chargeArrowRenderer = chargeArrow.GetComponent<SpriteRenderer>();
			chargeArrowRenderer.enabled = false;

			depositHole = GameObject.Find("DepositHole");

		}
	}

	// Update is called once per frame
	void Update()
	{
		lastPosition = transform.position;

		if (currentState == NPCState.IDLE)
		{
			spriteRenderer.sprite = spriteArray[0];
		}
		else if (currentState == NPCState.WALKING || currentState == NPCState.RUNNING)
		{
			if (randomTimeChange < 0 || (transform.position.x == currentTarget.x && transform.position.y == currentTarget.y))
			{
				currentTarget = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-4.3f, 6.0f), 0);
				randomTimeChange = 5.0f;
			}
			else
			{
				randomTimeChange -= Time.deltaTime;
			}

			if (transform.position.x + currentTarget.x > transform.position.x)
			{
				spriteRenderer.flipX = true;
			}
			else
			{
				spriteRenderer.flipX = false;
			}
			if (currentState == NPCState.WALKING)
			{
				WalkAnimation(0.5f);
				transform.position = Vector3.MoveTowards(transform.position, currentTarget, walkSpeed * Time.deltaTime);
			}
			else if (currentState == NPCState.RUNNING)
			{
				WalkAnimation(0.2f);
				transform.position = Vector3.MoveTowards(transform.position, currentTarget, runSpeed * Time.deltaTime);
			}
		}
		else if (currentState == NPCState.FOLLOWING || currentState == NPCState.CHASING)
		{

			currentTarget = player.transform.position;

			if (currentState == NPCState.FOLLOWING)
			{
				WalkAnimation(0.5f);
				transform.position = Vector3.MoveTowards(transform.position, currentTarget, walkSpeed * Time.deltaTime);
			}
			else if (currentState == NPCState.CHASING)
			{
				WalkAnimation(0.2f);
				transform.position = Vector3.MoveTowards(transform.position, currentTarget, runSpeed * Time.deltaTime);
			}
		}
		if (gameObject.name != "Shopkeeper")
		{
			GameObject chest = GetClosetChest();
			if (chest != null || heldChest != null)
			{
				currentState = NPCState.COLLECTING;
			}


			if (currentState == NPCState.ATTACK)
			{

				if (transform.position.x + currentTarget.x > transform.position.x)
				{
					spriteRenderer.flipX = true;
				}
				else
				{
					spriteRenderer.flipX = false;
				}

				if (chargeTime > 0)
				{
					WalkAnimation(0.05f);
					currentTarget = player.transform.position;
					chargeArrowRenderer.enabled = true;
					chargeTime -= Time.deltaTime;

					Vector2 direction = currentTarget - chargeArrow.transform.position;
					float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
					chargeArrow.transform.rotation = Quaternion.Slerp(chargeArrow.transform.rotation, rotation, chargeSpeed * Time.deltaTime);
				}
				else
				{
					chargeArrowRenderer.enabled = false;
					WalkAnimation(0.15f);
					transform.position = Vector3.LerpUnclamped(transform.position, currentTarget, chargeSpeed * Time.deltaTime);
					if (Vector2.Distance(transform.position, currentTarget) <= 0.1f)
					{
						chargeTime = 3.0f;
					}
				}
			}
			else if (currentState == NPCState.COLLECTING)
			{
				chargeArrowRenderer.enabled = false;
				if (chest == null && heldChest == null)
				{
					currentState = NPCState.ATTACK;
				}
				else
				{
					if (heldChest == null)
					{
						currentTarget = chest.transform.position;
						previousGoldCount = -1;
					}
					else if (heldChest.GetComponent<Box>().maxSize <= heldChest.GetComponent<Box>().currentHoldings)
					{
						if (currentTarget != depositHole.transform.position)
						{
							currentTarget = depositHole.transform.position;
							currentChestDepostits++;
						}
					}
					else
					{
						if (heldChest.GetComponent<Box>().currentHoldings % 10 != 0)
						{
							currentTarget = GetClosetGold();
						}
						else 
						{
							if (previousGoldCount != heldChest.GetComponent<Box>().currentHoldings)
							{
								currentTarget = GetRandomGold();
							}
							previousGoldCount = heldChest.GetComponent<Box>().currentHoldings;
						}
						if (currentTarget.x == 0 && currentTarget.y == 0)
						{
							currentTarget = GetRandomGold();
						}
					}



					if (transform.position.x + currentTarget.x > transform.position.x)
					{
						spriteRenderer.flipX = true;
					}
					else
					{
						spriteRenderer.flipX = false;
					}

					WalkAnimation(0.2f);
					transform.position = Vector3.Lerp(transform.position, currentTarget, collectSpeed * Time.deltaTime);
				}
			}

			if (currentChestDepostits >= maxChestDeposits && heldChest == null)
			{
				Destroy(gameObject);
			}
		}

		
	}

	private GameObject GetClosetChest()
	{
		GameObject foundChest = null;
		ArrayList allChest = new ArrayList();
		allChest.AddRange(GameObject.FindGameObjectsWithTag("Chest"));
		

		Vector3 position = transform.position;
		Vector3 playerDiff = player.transform.position - position;
		float playerDistance = playerDiff.sqrMagnitude;

		foreach (GameObject go in allChest)
		{
			if (!go.GetComponent<Box>().IsHeld)
			{
				Vector3 diff = go.transform.position - position;
				float curDistance = diff.sqrMagnitude;
				if (curDistance < playerDistance)
				{
					foundChest = go;
				}
			}
		}
		return foundChest;
	}

	private Vector3 GetClosetGold()
	{
		Vector3 gold = Vector3.zero;
		ArrayList allGold = new ArrayList();
		allGold.AddRange(GameObject.FindGameObjectsWithTag("Gold"));

		float distance = Mathf.Infinity;
		foreach (GameObject go in allGold)
		{
			Vector3 diff = go.transform.position - transform.position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance)
			{
				distance = curDistance;
				gold = go.transform.position;
			}
		}
		return gold;
	}

	private Vector3 GetRandomGold()
	{
		Vector3 gold = Vector3.zero;
		ArrayList allGold = new ArrayList();
		allGold.AddRange(GameObject.FindGameObjectsWithTag("Gold"));

		gold = ((GameObject)allGold[Random.Range(0, allGold.Count - 1)]).transform.position;

		return gold;
	}

	private void WalkAnimation(float newTime)
	{
		if (animationTime < 0)
		{
			if (currentFrame == 1)
			{
				currentFrame = 2;
			}
			else
			{
				currentFrame = 1;
			}
			animationTime = newTime;
			spriteRenderer.sprite = spriteArray[currentFrame];
		}
		else
		{
			animationTime -= Time.deltaTime;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (gameObject.name != "Shopkeeper")
		{
			if (heldChest == null)
			{
				if (collision.gameObject.tag.Equals("Chest") && collision.gameObject.transform.parent == null)
				{
					if (collision.gameObject.GetComponent<Box>().heldCoolDown <= 0)
					{
						heldChest = collision.gameObject;
						heldChest.transform.SetParent(this.gameObject.transform);
						Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
					}
				}
			}
		}
		else if (collision.gameObject.tag.Equals("Chest"))
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
		}
		else if (collision.gameObject.tag.Equals("Map"))
		{
			transform.position = lastPosition;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (gameObject.name != "Shopkeeper")
		{
			if (heldChest == null)
			{
				if (collision.gameObject.tag.Equals("Chest") && collision.gameObject.transform.parent == null)
				{
					if (collision.gameObject.GetComponent<Box>().heldCoolDown <= 0)
					{
						heldChest = collision.gameObject;
						heldChest.transform.SetParent(this.gameObject.transform);
						Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
					}
				}
			}
		}
		if (collision.gameObject.tag.Equals("Map"))
		{
			transform.position = lastPosition;
		}
	}
}

public enum NPCState
{
	IDLE = 0,
	WALKING = 1,
	RUNNING = 2,
	FOLLOWING = 3,
	CHASING = 4,
	ATTACK = 5,
	COLLECTING = 6
}
