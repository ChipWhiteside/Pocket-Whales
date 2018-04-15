using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetScript : MonoBehaviour, SplashInterface {

	/*
	 * Every active splash
	 */
	private GameObject[] allSplashes;

	/*
	 * Energy whale loses when hit by this splash 
	 */
	private int energyEffect;

	/*
	 * Time the splash can stay active 
	 */
	private float maxActiveTime;

	/*
	 * Time until special effect if this splash has one
	 */
	private float timeUntilEffect;

	/*
	 * Counter until special effect
	 */
	private float effectTimer;

	/*
	 * counts up so the splash eventually disappears 
	 */
	private float despawnTimer;

	/*
	 * Reference to the control script
	 */ 
	private ControlScript controlScript;

	/*
	 * SplashManager Game Object
	 */
	public GameObject splashManager;

	/*
	 * SplashManagerScript
	 */
	private SplashManagerScript splashManagerScript;

	/*
	 * The number of carpetSplashes that have been dropped
	 */
	private int carpetsLaunched;

	/*
	 * If the carpet bombs have dropped yet
	 */
	private bool carpetsDropped;

	/*
	 * 
	 */
	private float timer;

	/*
	 * Controller Game Object
	 */
	public GameObject control;

	/*
	 * Is the turn already ending
	 */
	private bool endingTurn;

	/*
	 * Splashes launched after tap
	 */
	public GameObject carpetSplash;

	/*
	 * Cost to fire this splash
	 */
	private float cost = 50.0f;


	// Use this for initialization
	void Start () {
		carpetsLaunched = 0;
		carpetsDropped = false;
		energyEffect = 1;
		maxActiveTime = 20;
		despawnTimer = 0; //always starts at zero
		effectTimer = 0; //always starts at zero
		timeUntilEffect = 0; //when the special effect should happen
		controlScript = control.GetComponent<ControlScript> ();
		splashManagerScript = splashManager.GetComponent<SplashManagerScript> ();
		endingTurn = false;

		EffectOnLaunch ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetMouseButtonDown (0))
			EffectOnTap ();
		if (carpetsDropped && timer >= .035f && carpetsLaunched < 10) {
			DropBombs ();
		}
		despawnTimer += Time.deltaTime;
		if (despawnTimer >= maxActiveTime)
			EndTurn ();
	}

	public void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.CompareTag("Terrain")) {
			EffectOnBounce ();
		}
		if (collision.gameObject.CompareTag("Splash")) {
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		if (collision.gameObject.CompareTag("Whale")) {
			EffectOnHit (collision.gameObject);
		}
		if (collision.gameObject.CompareTag ("OutOfBounds")) {
			EndTurn ();
		}
	}

	public void EffectOnLaunch () {
		splashManagerScript.AddToSplashes (gameObject);
	}

	public void EffectOnTime () {

	}

	public void EffectOnHit (GameObject whale) {
		whale.GetComponent<WhaleControllerInterface> ().LoseEnergy (energyEffect);
		StartCoroutine(WaitToEndTurn(4f)); 
	}

	public void EffectOnTap () {
		carpetsDropped = true;
		splashManagerScript.RemoveFromSplashes (gameObject);
		StartCoroutine (CheckSplashManager());
	}

	void DropBombs(){
		timer = 0;
		carpetsLaunched++;
		Launch (270f, 500f, 0, -2);
	}

	private void Launch(float angle, float power, float xStart, float yStart) {
		Vector3 pos = new Vector3 (xStart, yStart, 0);
		GameObject splash = Instantiate (carpetSplash, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
		splash.SetActive (true);
		Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
		Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
		splashrb.AddForce(dir*power);
	}

	public void EffectOnBounce () {

	}

	/**
	 * Destroys all splashes, then switches the turn to the other whale
	 */
	public void EndTurn() {
		print ("end turn");
		print (endingTurn);
		if (!endingTurn) { //so we only try to end the turn once
			endingTurn = true;
			Destroy (gameObject);
			splashManagerScript.DestroySplashes ();
			controlScript.SwitchPlayerControl ();
		}
	}

	IEnumerator WaitToEndTurn(float time){
		yield return new WaitForSeconds (time);
		EndTurn();
	}

	/**
	 * When the player clicks/taps the screen
	 */
	void OnMouseDown() {
		EffectOnTap ();
	}

	/*
	 * If all the dropped splashes have been destroyed
	 */
	IEnumerator CheckSplashManager(){
		bool hold = true;
		while (hold){
			print ("Checking splashManager");
			yield return new WaitForSeconds(1f);
			print (splashManagerScript.IsEmpty ());
			if (splashManagerScript.IsEmpty ()) {
				EndTurn ();
				hold = false;
			}
		}
	}

	public float getCost () {
		return cost;
	}
}
