using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartCompController : MonoBehaviour, WhaleControllerInterface {

	private int energy;

	public Slider energySlider;

	private Rigidbody2D rb;

	private SpriteRenderer sr;

	private Sprite whaleActive;

	private Sprite whaleIdle;

	//public float force;

	public GameObject playerWhale;

	public GameObject projectile;

	private GameObject controller;

	private ControlScript control;

	private LaunchSplash launchScript;

	public float initialAngle;

	public string name;

	public int playerNo; // 1 is the player, 2 is the computer

	public bool isComputerWhale;

	private bool compMoved;

	private float rangeLeft = -3f; //inclusive
	private float rangeRight = 3f; //inclusive

	public GameObject angleAimPoint; //how the AI will find the angle to shoot to make it over the mountain


	void Start ()
	{
		energy = 100;
		energySlider.value = energy;
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		whaleActive = Resources.Load<Sprite>("Whale_Active");
		whaleIdle = Resources.Load<Sprite> ("Whale_Idle");
		controller = GameObject.Find("Controller");
		control = controller.GetComponent<ControlScript>();
	}

	void Update ()
	{

		if (control.turn == 2 && !control.looping && compMoved) {
			Vector3 pos = new Vector3 (0, 1, 0); //position of the computer whale but one up

			gameObject.GetComponent<Rigidbody2D> ().isKinematic = true; //so the whale doesn't move or get hit during its turn
			gameObject.GetComponent<Collider2D> ().enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
			gameObject.GetComponent<Rigidbody2D> ().freezeRotation = true;

			GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
			splash.SetActive (true);
			Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();

			float rangeX = Random.Range (rangeLeft, rangeRight);
			Vector3 range = new Vector3 (rangeX, 0, 0);
			print ("rangeX = " + rangeX);
			Vector3 vector = CalculateTrajectoryVelocity(transform.position, playerWhale.transform.position + range, 5);
			splashrb.velocity = vector; //FIRE!
			control.TakeControl (control.turn);

			//FireProjectile ();
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
			waitForTime();
			float dir = Random.Range (0, 2);
			if (dir == 0)
				control.MoveR (rb);
			else
				control.MoveL (rb);
			compMoved = true;
		}
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

	Vector3 CalculateTrajectoryVelocity(Vector3 origin, Vector3 target, float t)
	{
		float vx = (target.x - origin.x) / t;
		float vz = (target.z - origin.z) / t;
		float vy = ((target.y - origin.y) - 0.5f * Physics.gravity.y * t * t) / t;
		return new Vector3(vx, vy, vz);
	}

	/*
	 * Used in Splashes to tell if PlayerScript or SmartCompScript is needed
	 */
	public bool IsComputer() {
		return isComputerWhale;
	}

	public void LoseEnergy(int lostEnergy) {
		energy -= lostEnergy;
		energySlider.value = energy;
		if (energy <= 0) 
			control.EndGame (name);
	}


	/*
	 * void FireProjectile() {
		//Transform target;

		//float initialAngle;
		Vector3 pos = new Vector3 (0, 1, 0); //position of the computer whale but one up
		GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
		splash.SetActive (true);
		Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();

		var rigid = GetComponent<Rigidbody>();

		Vector3 p = playerWhale.transform.position; //getting the player whales position as Vector 3

		float gravity = Physics.gravity.magnitude; //finding value of gravity


		float angle = initialAngle * Mathf.Deg2Rad; // Selected angle in radians

		print ("angle = " + angle);

		// Positions of the player whale and the computer whale on the same plane
		Vector3 planarTarget = new Vector3(p.x, 0, p.z); //player whale
		Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z); //computer whale (this)

		float distance = Vector3.Distance(planarTarget, planarPostion); //distance between objects

		//increases the hit range if the computer his the player whale last shot, decreases if comp missed;
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

		float yOffset = transform.position.y - p.y; //distance along the y axis between objects

		print ("yoffset = " + yOffset);

		float initialVelocity = -1 * (-1 * (-1 / Mathf.Cos(angle))) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (-1 * (distance * Mathf.Tan(angle) + yOffset)));
		print ("Mathf.Cos(angle) = " + Mathf.Cos (angle).ToString());
		print ("Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) = " + Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2))));
		print ("(distance * Mathf.Tan(angle) + yOffset) = " + (distance * Mathf.Tan (angle) + yOffset).ToString());

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
		StartCoroutine (WaitUntilInactive(splash, splashrb, control));
	}
	*/
}
