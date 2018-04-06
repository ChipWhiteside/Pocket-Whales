using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSprinkler : MonoBehaviour {

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
	 * 
	 */
	public GameObject control;

	/*
	 * 
	 */
	private ControlScript controlScript;

	// Use this for initialization
	void Start () {
		energyEffect = 10;
		maxActiveTime = 10;
		despawnTimer = 0;
		controlScript = control.GetComponent<ControlScript> ();
		EffectOnLaunch ();
	}

	// Update is called once per frame
	void Update () {
		despawnTimer += Time.deltaTime;
		effectTimer += Time.deltaTime;
		if (despawnTimer >= maxActiveTime)
			EndTurn ();
		if (effectTimer >= timeUntilEffect)
			EffectOnTime ();
	}

	void OnCollisionEnter(Collision2D collision){
		if (collision.gameObject.CompareTag("Terrain")) {
			EffectOnBounce ();
		}
		if (collision.gameObject.CompareTag("Splash")) {
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		if (collision.gameObject.CompareTag("Whale")) {
			EffectOnHit ();
		}
	}

	void EffectOnLaunch() {

	}

	void EffectOnTime() {
		
	}

	void EffectOnHit() {
		gameObject.GetComponent<Rigidbody2D> ().Sleep ();
	}

	void EffectOnBounce() {
		gameObject.GetComponent<Rigidbody2D> ().Sleep ();
	}

	void EndTurn() {
		Destroy (gameObject);
		controlScript.SwitchPlayerControl ();
	}

}
