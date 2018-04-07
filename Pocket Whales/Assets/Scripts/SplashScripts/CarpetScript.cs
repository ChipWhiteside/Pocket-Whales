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

	// Use this for initialization
	void Start () {
		carpetsLaunched = 0;
		carpetsDropped = false;
		energyEffect = 10;
		maxActiveTime = 20;
		despawnTimer = 0; //always starts at zero
		effectTimer = 0; //always starts at zero
		timeUntilEffect = 0; //when the special effect should happen
		controlScript = control.GetComponent<ControlScript> ();
		endingTurn = false;

		EffectOnLaunch ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetMouseButtonDown (0))
			EffectOnTap ();
		despawnTimer += Time.deltaTime;
		if (despawnTimer >= maxActiveTime)
			EndTurn ();
	}

	public void OnCollisionEnter2D (Collision2D collision) {

	}

	public void EffectOnLaunch () {

	}

	public void EffectOnTime () {

	}

	public void EffectOnHit (GameObject whale) {
		//could change playerController and SmartCompController to implement an interface so this would only need to be one line
		whale.GetComponent<WhaleControllerInterface> ().LoseEnergy (energyEffect); 
	}

	public void EffectOnTap () {
		print ("carpetsDropped 1: " + carpetsDropped);
		if (!carpetsDropped) {
			print ("carpetsDropped 2: " + carpetsDropped);
			while (carpetsLaunched < 5) {
				if (timer >= .3f) {
					timer = 0;
					Launch (270f, 500f, 0, -1);
					carpetsLaunched++;
				}
			}
			carpetsDropped = true;
		}
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
		if (!endingTurn) { //so we only try to end the turn once
			endingTurn = true;
			Destroy (gameObject);
			DestroyAllObjects ();
			controlScript.SwitchPlayerControl ();
		}
	}

	/**
	 * When the player clicks/taps the screen
	 */
	void OnMouseDown() {
		EffectOnTap ();
	}

	void DestroyAllObjects()
	{
		allSplashes = GameObject.FindGameObjectsWithTag ("Splash");

		for(var i = 0 ; i < allSplashes.Length ; i++)
		{
			Destroy(allSplashes[i]);
		}
	}

	IEnumerator Wait(float time) {
		yield return new WaitForSeconds(time);
	}

}
