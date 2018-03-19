using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	private SpriteRenderer sr;

	private Sprite whaleActive;

	private Sprite whaleIdle;

	public float force;

	public GameObject projectile;

	public GameObject testProjectile;

	private GameObject controller;

	private ControlScript control;

	public LaunchSplash launchScript;

	public float angle;

	public int playerNo; // 1 is the player, 2 is the computer

	private bool compMoved = false;

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
			
		if (Input.GetKeyDown ("space") && control.turn == playerNo && !control.looping) {
			//print("space key was pressed");
			Vector3 pos = new Vector3 (0, 1, 0);
			GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
			splash.SetActive (true);
			sr.sprite = whaleActive;
			StartCoroutine(LaunchAnimation());
			Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			splashrb.AddForce(dir*force);
			control.TakeControl (control.turn);
			StartCoroutine (WaitUntilInactive(splash, splashrb, control));
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
		if (Input.GetKeyDown ("right")){
			if (playerNo == control.turn && control.canMove)
				control.MoveR (rb);
		}
		if (Input.GetKeyDown ("left")) {
			if (playerNo == control.turn && control.canMove)
				control.MoveL (rb);
		}
	}

	/*
	 * Switches player control once the splash is finished acting
	 */
	IEnumerator WaitUntilInactive(GameObject obj, Rigidbody2D rb, ControlScript control) 
	{
		while (true) {
			yield return new WaitForSeconds(1.0f); //check once a second to see if the turn has ended
			//print("is sleeping?");
			if (rb.IsSleeping ()) {
				Destroy (obj);
				//print ("Destroyed");
				control.SwitchPlayerControl ();
				compMoved = false;
				yield break;
			}
		}
	}

	void shoot() {

	}
<<<<<<< HEAD
<<<<<<< HEAD

	void OnTriggerEnter(Collider projectile) {
		Destroy(projectile);
		control.playerHit = true;
		print ("playerHit = true");
	}

=======
>>>>>>> parent of eb6ba64... Merge branch 'developer' of https://github.com/ChipWhiteside/Pocket-Whales into developer
=======
>>>>>>> parent of eb6ba64... Merge branch 'developer' of https://github.com/ChipWhiteside/Pocket-Whales into developer
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

	/*
	IEnumerator DestroyObject(GameObject obj) {
		yield return new WaitForSeconds(0.5f);
		Destroy (obj);
	}
	*/

	IEnumerator LaunchAnimation() {
		yield return new WaitForSeconds(0.5f);
		sr.sprite = whaleIdle;
	}



	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
		}
	}*/

}
