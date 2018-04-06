﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompController : MonoBehaviour {

	private Rigidbody2D rb;

	private SpriteRenderer sr;

	private Sprite whaleActive;

	private Sprite whaleIdle;

	//public float force;

	public GameObject playerWhale;

	public GameObject projectile;

	public GameObject testProjectile;

	private GameObject controller;

	private ControlScript control;

	private LaunchSplash launchScript;

	public float initialAngle;

	public int playerNo; // 1 is the player, 2 is the computer

	private bool compMoved;


	private float rangeLeft = -6f; //inclusive
	private float rangeRight = 7f; //exclusive

	public GameObject angleAimPoint; //how the AI will find the angle to shoot to make it over the mountain



	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		whaleActive = Resources.Load<Sprite>("Whale_Active");
		whaleIdle = Resources.Load<Sprite> ("Whale_Idle");
		controller = GameObject.Find("Controller");
		control = controller.GetComponent<ControlScript>();
		//InvokeRepeating("LaunchTestProjectile", 0.3f, 0.2f); //for the sight line
		//Time.timeScale = 0.5f;
	}

	void Update ()
	{
			
		if (control.turn == 2 && !control.looping && compMoved) {
			
			FireProjectile ();

	
			/*
			//Pause (100000f);
			//yield WaitForSeconds (0.25);
			Vector3 pos = new Vector3 (0, 1, 0);
			GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
			splash.SetActive (true);
			sr.sprite = whaleActive;
			StartCoroutine(LaunchAnimation());
			Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
			Vector3 dir = Quaternion.AngleAxis(initialAngle, Vector3.forward) * Vector3.right;
			splashrb.AddForce(dir*force);
			control.TakeControl (control.turn);
			StartCoroutine (WaitUntilInactive(splash, splashrb, control));
			*/
		}
			
	}

	/*
	 * Wait: pauses decision for 1 second(s) to make it feel like player reflexes even though its AI
	*/
	IEnumerator waitForTime()
	{
		yield return new WaitForSeconds(1);
	}

	void FixedUpdate ()
	{
		if (control.turn == 2 && control.canMove) {
			Pause (4);
			float dir = Random.Range (0, 2);
			if (dir == 0)
				control.MoveR (rb);
			else
				control.MoveL (rb);
			compMoved = true;
		}
	}

	void shoot() {

	}

	public IEnumerator Pause(float p)
	{
		Time.timeScale = 0.1f;
		float pauseEndTime = Time.realtimeSinceStartup + p;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1;
	}
	/*
	 * Fires a test shot every .2 seconds that dissapears after .5 seconds.
	 * 
	void LaunchTestProjectile()
	{
		if(playerNo == control.turn)
		{
			Vector3 pos = new Vector3 (0, 1, 0);
			GameObject fakeSplash = Instantiate (testProjectile, transform.position + pos, transform.rotation);
			fakeSplash.SetActive (true);
			Rigidbody2D splashrb = fakeSplash.GetComponent<Rigidbody2D> ();
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			splashrb.AddForce(dir*force);
			StartCoroutine(DestroyObject(fakeSplash));
		}
	}
*/
	IEnumerator DestroyObject(GameObject obj) {
		yield return new WaitForSeconds(0.5f);
		Destroy (obj);
	}

	IEnumerator LaunchAnimation() {
		yield return new WaitForSeconds(0.5f);
		sr.sprite = whaleIdle;
	}

	void FireProjectile() {
		//Transform target;

		//float initialAngle;
		Vector3 pos = new Vector3 (0, 1, 0); //position of the computer whale
		GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
		splash.SetActive (true);
		Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();

		var rigid = GetComponent<Rigidbody>();

		Vector3 p = playerWhale.transform.position;

		float gravity = Physics.gravity.magnitude;
		// Selected angle in radians
		float angle = initialAngle * Mathf.Deg2Rad;
		print ("angle = " + angle);

		// Positions of this object and the target on the same plane
		Vector3 planarTarget = new Vector3(p.x, 0, p.z);
		Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);

		// Planar distance between objects
		float distance = Vector3.Distance(planarTarget, planarPostion);


		if (control.playerHit) {
			rangeLeft -= 2;
			rangeRight += 2;
		} else {
			if (rangeLeft < 0 && rangeRight > 1) {
				rangeLeft += 2;
				rangeRight -= 2;
			}
		}
		print ("Range: [" + rangeLeft + "," + rangeRight + "]");

		//give a range around the whale where the AI will hit
		distance = (distance + Random.Range(rangeLeft, rangeRight));

		print ("distance = " + distance);
		// Distance along the y axis between objects
		float yOffset = transform.position.y - p.y;
		print ("yoffset = " + yOffset);

		float initialVelocity = -1 * (-1 * (-1 / Mathf.Cos(angle))) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (-1 * (distance * Mathf.Tan(angle) + yOffset)));
		print ("Mathf.Cos(angle) = " + Mathf.Cos (angle).ToString("n#.########################"));
		print ("Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) = " + Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2))));
		print ("(distance * Mathf.Tan(angle) + yOffset) = " + (distance * Mathf.Tan (angle) + yOffset).ToString("##################"));
	
		print ("InitialVelocity = " + initialVelocity);

		Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
		print ("Velocity = " + velocity);

		// Rotate our velocity to match the direction between the two objects
		float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion);
		Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
		print ("Final Velocity = " + finalVelocity);
		print ("Splashrb.mass = " + splashrb.mass);

		// Fire!
		splashrb.AddForce(finalVelocity * splashrb.mass, ForceMode2D.Impulse);
		control.TakeControl (control.turn);
	}



	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
		}
	}*/

}
