using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPlayer : MonoBehaviour
{
	public float speed = 5f;
	private Rigidbody2D rigid2;
	public GameObject body;
	private SpriteRenderer bodySpriteRenderer;
	private Animator bodyAnimator;

	public float throwForce;

	public GameObject chest;

	private float pickupCoolDown;

	private bool allowShopOpen;

	private GameObject shopCanvas;
	private GameObject panel1;
	private GameObject panel2;

	// Use this for initialization
	void Start ()
	{
		rigid2 = gameObject.GetComponent<Rigidbody2D>();
		bodySpriteRenderer = body.GetComponent<SpriteRenderer>();
		bodyAnimator = body.GetComponent<Animator>();
		shopCanvas = GameObject.Find("ShopUICanvas");
		panel1 = shopCanvas.transform.Find("Panel1").gameObject;
		panel2 = shopCanvas.transform.Find("Panel2").gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//float moveHorizontal = Input.GetAxis("Horizontal");

		//float moveVertical = Input.GetAxis("Vertical");

		//Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		//rigid2.AddForce(movement * speed);
		transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.D))
		{
			bodySpriteRenderer.flipX = true;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			bodySpriteRenderer.flipX = false;
		}

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			bodyAnimator.Play(1);
		}
		else
		{
			bodyAnimator.Play(0);
		}

		if (pickupCoolDown <= 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Transform chest = transform.Find("Chest");
				if (chest != null)
				{
					Vector3 mouse = Input.mousePosition;
					mouse.z = 10.0f;
					mouse = Camera.main.ScreenToWorldPoint(mouse);


					Box box = chest.GetComponent<Box>();
					box.IsHeld = false;
					box.heldCoolDown = 1.0f;
					chest.GetComponent<Rigidbody2D>().AddForce((mouse - chest.transform.position) * throwForce);
				}
			}
		}
		else
		{
			pickupCoolDown -= Time.deltaTime;
		}

		if (allowShopOpen)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				panel1.SetActive(true);
			}
		}
		else
		{
			panel1.SetActive(false);
			panel2.SetActive(false);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Chest") && chest ==null && collision.gameObject.transform.parent == null)
		{
			if (collision.gameObject.GetComponent<Box>().heldCoolDown <= 0)
			{
				if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
				{
					pickupCoolDown = 1.0f;
					chest = collision.gameObject;
					chest.transform.parent = this.gameObject.transform;
					Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
				}
			}
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Chest") && chest == null && collision.gameObject.transform.parent == null)
		{
			if (collision.gameObject.GetComponent<Box>().heldCoolDown <= 0)
			{
				if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
				{
					pickupCoolDown = 1.0f;
					chest = collision.gameObject;
					chest.transform.parent = this.gameObject.transform;
					Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Shopkeeper")
		{
			allowShopOpen = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Shopkeeper")
		{
			allowShopOpen = false;
		}
	}
}
