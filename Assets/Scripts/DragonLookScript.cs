using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLookScript : MonoBehaviour
{
	public float speed = 5f;
	public Sprite headUp;
	public Sprite headDown;
	public Sprite headSide;
	public Sprite headSideDown;
	public Sprite headSideUp;
	public SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start ()
	{
		spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
		if (angle > -22.5 && angle <= 22.5)
		{
			spriteRenderer.sprite = headSide;
			spriteRenderer.flipX = true;
		}
		else if (angle > 22.5 && angle <= 67.5)
		{
			spriteRenderer.sprite = headSideUp;
			spriteRenderer.flipX = true;
		}
		else if (angle > 67.5 && angle <= 112.5)
		{
			spriteRenderer.sprite = headUp;
		}
		else if (angle > 112.5 && angle <= 157.5)
		{
			spriteRenderer.sprite = headSideUp;
			spriteRenderer.flipX = false;
		}
		else if (angle > 157.5 && angle <= 180 || angle >= -180 && angle < -157.5) //202.5 - 180 = 22.5
		{
			spriteRenderer.sprite = headSide;
			spriteRenderer.flipX = false;
		}
		else if (angle > -157.5 && angle <= -112.5)
		{
			spriteRenderer.sprite = headSideDown;
			spriteRenderer.flipX = false;
		}
		else if (angle > -112.5 && angle <= -67.5)
		{
			spriteRenderer.sprite = headDown;
		}
		else if (angle > -67.5 && angle <= -22.5)
		{
			spriteRenderer.sprite = headSideDown;
			spriteRenderer.flipX = true;
		}
		//Debug.Log("Angle: " + angle);
	}
}
