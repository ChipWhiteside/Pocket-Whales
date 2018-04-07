using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerScript : MonoBehaviour, SplashInterface {

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
	 * Controller Game Object
	 */
	public GameObject control;

	/*
	 * ControlScript
	 */
	private ControlScript controlScript;

	/*
	 * Is the turn already ending
	 */
	private bool endingTurn;

	/*
	 * How many times has it bounced
	 */
	private int bounceCount;

	private Vector2 firstBounceVelocityNormalized;

	// Use this for initialization
	void Start () {
		energyEffect = 10;
		maxActiveTime = 20;
		despawnTimer = 0; //always starts at zero
		effectTimer = 0; //always starts at zero
		timeUntilEffect = 0; //when the special effect should happen
		controlScript = control.GetComponent<ControlScript> ();
		endingTurn = false;
		bounceCount = 0;

		EffectOnLaunch ();
	}

	// Update is called once per frame
	void Update () {
		despawnTimer += Time.deltaTime;
		if (despawnTimer >= maxActiveTime)
			EndTurn ();
	}

	public void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag("Terrain")) {
			EffectOnBounce ();
		}
		if (collision.gameObject.CompareTag("Splash")) {
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		if (collision.gameObject.CompareTag("Whale")) {
			EffectOnHit (collision.gameObject);
		}
	}

	public void EffectOnLaunch() {

	}

	public void EffectOnTime() {

	}

<<<<<<< HEAD
	public void EffectOnHit(GameObject whale) {
		
		whale.GetComponent<WhaleControllerInterface> ().LoseEnergy (energyEffect); //could change playerController and SmartCompController to implement an interface so this would only need to be one line
=======
	public void EffectOnHit() {
>>>>>>> 37462a273cab96ca3f94811f87bb3e0fcfc8a83e
		if (bounceCount == 0) {

		}
		
		if(bounceCount >= 7){
			EndTurn ();
		}
		bounceCount++;
	}

	public void EffectOnBounce() {
		
		if(bounceCount >= 7){
			EndTurn ();
		}
		bounceCount++;
	}

	public void EffectOnTap() {

	}

	public void EndTurn() {
		if (!endingTurn) { //so we only try to end the turn once
			endingTurn = true;
			Destroy (gameObject);
			DestroyAllObjects ();
			controlScript.SwitchPlayerControl ();
		}
	}

	void DestroyAllObjects()
	{
		allSplashes = GameObject.FindGameObjectsWithTag ("Splash");
		for(var i = 0 ; i < allSplashes.Length ; i++)
		{
			Destroy(allSplashes[i]);
		}
	}

}
