using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThing : MonoBehaviour
{

	public float speed = 5f;
	private float clicked;
	private float clicktime;
	private float clickdelay;

	public GameObject Fireball;
	public float fireBallForce;
	// Use this for initialization
	void Start ()
	{
		clicked = 0;
		clicktime = 0;
		clickdelay = 0.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

		if (DoubleClick())
		{
			GameObject fireBall = Instantiate(Fireball, transform.position, transform.rotation);
			Vector3 mouse = Input.mousePosition;
			mouse.z = 10.0f;
			mouse = Camera.main.ScreenToWorldPoint(mouse);
			fireBall.GetComponent<Rigidbody2D>().AddForce((mouse - fireBall.transform.position) * fireBallForce);
		}
	}

	bool DoubleClick()
	{
		if (Input.GetMouseButtonDown(1))
		{
			clicked++;
			if (clicked == 1) clicktime = Time.time;
		}
		if (clicked > 1 && Time.time - clicktime < clickdelay)
		{
			clicked = 0;
			clicktime = 0;
			return true;
		}
		else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
		return false;
	}
}
