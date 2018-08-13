using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	public float lifeTime;
	private bool explode;
	public float explodeTime;
	private BoxCollider2D boxCollider;
	private Rigidbody2D rigi;
	private Animator animator;
	private AudioSource audioSource;
	public AudioClip explosionClip;

	private GameObject dragon;
	// Use this for initialization
	void Start ()
	{
		lifeTime = 1.0f;
		boxCollider = transform.GetComponent<BoxCollider2D>();
		rigi = transform.GetComponent<Rigidbody2D>();
		animator = transform.GetComponent<Animator>();
		audioSource = transform.GetComponent<AudioSource>();
		dragon = GameObject.Find("Dragon");
		Physics2D.IgnoreCollision(boxCollider, dragon.GetComponent<BoxCollider2D>());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (explode)
		{
			if (explodeTime > 0)
			{
				if (!audioSource.isPlaying || audioSource.clip != explosionClip)
				{
					audioSource.clip = explosionClip;
					audioSource.PlayOneShot(explosionClip);
				}
				animator.Play("Explode");
				boxCollider.size = new Vector2(boxCollider.size.x * 1.01f, boxCollider.size.y * 1.01f);
			}
			else
			{
				Destroy(gameObject);
			}
			explodeTime -= Time.deltaTime;
		}
		if (lifeTime < 0)
		{
			explode = true;
		}
		lifeTime -= Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Gold" || collision.gameObject.tag == "Map")
		{
			explode = true;
			rigi.velocity = Vector2.zero;
			rigi.isKinematic = true;
		}
		if (collision.gameObject.tag == "Fire" || collision.gameObject.name == "Shopkeeper")
		{
			Physics2D.IgnoreCollision(collision.collider, boxCollider);
		}
	}
}
