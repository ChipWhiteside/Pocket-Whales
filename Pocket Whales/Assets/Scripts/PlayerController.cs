using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, WhaleControllerInterface {

	private int energy;

	public Slider energySlider;

	private Rigidbody2D rb;

	private SpriteRenderer sr;

	private Sprite whaleActive;

	private Sprite whaleIdle;

	public float force;

	public GameObject projectile;

	private GameObject controller;

	private ControlScript control;

	private LaunchSplash launchScript;

	public float angle;

	public string name; //Hopefully UserName for the player

	public int playerNo; // 1 is the player, 2 is the computer

	private bool compMoved = false;

	public float money = 1000.0f;

	void Start ()
	{
		name = "Player1";
		energy = 100;
		energySlider.value = energy;
		//rb = whaleComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		whaleActive = Resources.Load<Sprite>("Whale_Active");
		whaleIdle = Resources.Load<Sprite> ("Whale_Idle");
		controller = GameObject.Find("Controller");
		control = controller.GetComponent<ControlScript>();
	}

	void Update ()
	{
			
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

	public void Launch(float angle, float power, GameObject splashOption) {
		//float moneyNeeded = projectile.GetComponent<SplashInterface> ().getCost ();
		//if (money >= moneyNeeded) {
			if (control.turn == playerNo && !control.looping) {
				//money -= moneyNeeded;
				Vector3 pos = new Vector3 (0, 0, 0);

				gameObject.GetComponent<Rigidbody2D> ().isKinematic = true; //so the whale doesn't move or get hit during its turn
				gameObject.GetComponent<Collider2D> ().enabled = false;
				gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
				gameObject.GetComponent<Rigidbody2D> ().freezeRotation = true;

				GameObject splash = Instantiate (splashOption, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
				splash.SetActive (true);
				sr.sprite = whaleActive;
				Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
				Vector3 dir = Quaternion.AngleAxis (angle, Vector3.forward) * Vector3.right;
				splashrb.velocity = dir * power;
				control.TakeControl (control.turn);
				compMoved = false;
				//StartCoroutine (WaitUntilInactive(splash, splashrb, control));
			}
		//} else {
		//	print ("Insufficient funds...\nNeeded: " + money + "   Current: " + moneyNeeded);
		//}
	}


	void OnTriggerEnter(Collider projectile) {
		Destroy(projectile);
		control.playerHit = true;
		print ("playerHit = true");
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

	/*
	IEnumerator DestroyObject(GameObject obj) {
		yield return new WaitForSeconds(0.5f);
		Destroy (obj);
	}
	*/

	public void LoseEnergy(int lostEnergy) {
		energy -= lostEnergy;
		energySlider.value = energy;
		if (energy <= 0) {
			control.EndGame (gameObject);
		}
	}

	public string GetName() {
		return name;
	}

	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
		}
	}*/

	public void GotAHit(float reward) {
		money += reward;
	}

	public void ChooseSplash() {

	}
}
