using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerScript : MonoBehaviour, SplashInterface {

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
	 * SplashManager Game Object
	 */
	public GameObject splashManager;

	/*
	 * SplashManagerScript
	 */
	private SplashManagerScript splashManagerScript;

	/*
	 * Is the turn already ending
	 */
	private bool endingTurn;

	/*
	 * Splashes Launched by Sprinkler
	 */
	public GameObject sprinklerSplash;

	private bool specialEffectStart;

	private float secondaryTimer;

	private float secondaryAngle;

	private bool secondaryAngleIncreasing;

	/*
	 * How many sprinkles have been launched
	 */
	private int sprinklesLaunched; 

	// Use this for initialization
	void Start () {
		energyEffect = 1;
		maxActiveTime = 20;
		despawnTimer = 0; //always starts at zero
		effectTimer = 0; //always starts at zero
		timeUntilEffect = 0; //when the special effect should happen
		controlScript = control.GetComponent<ControlScript> ();
		splashManagerScript = splashManager.GetComponent<SplashManagerScript> ();
		endingTurn = false;
		secondaryTimer = 0;
		secondaryAngle = 45;
		secondaryAngleIncreasing = true;
		specialEffectStart = false;
		sprinklesLaunched = 0;

		EffectOnLaunch ();
	}

	// Update is called once per frame
	void Update () {
		secondaryTimer += Time.deltaTime;
		if (secondaryTimer >= .3F && specialEffectStart) {
			secondaryTimer = 0;
			LaunchMore ();
		}
		despawnTimer += Time.deltaTime;
		effectTimer += Time.deltaTime;
		if (despawnTimer >= maxActiveTime)
			EndTurn ();
		if (effectTimer >= timeUntilEffect)
			EffectOnTime ();
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
		if (collision.gameObject.CompareTag ("OutOfBounds")) {
			EndTurn ();
		}
	}

	public void EffectOnLaunch() {
		splashManagerScript.AddToSplashes (gameObject);
		splashManagerScript.IgnoreSplashes (gameObject);
	}

	public void EffectOnTime() {
		
	}

	public void EffectOnHit(GameObject whale) {
		whale.GetComponent<WhaleControllerInterface> ().LoseEnergy (energyEffect);
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		specialEffectStart = true;
	}

	public void EffectOnBounce() {
		gameObject.GetComponent<Rigidbody2D> ().isKinematic = true;
		gameObject.GetComponent<Rigidbody2D> ().velocity = Vector3.zero;
		specialEffectStart = true;
	}

	public void EffectOnTap() {

	}

	public void EndTurn() {
		if (!endingTurn) { //so we only try to end the turn once
			endingTurn = true;
			StartCoroutine(WaitToEnd(4F));
		}
	}

	private void LaunchMore() {
		if (sprinklesLaunched >= 20)
			EndTurn ();
		sprinklesLaunched++;
		if(sprinklesLaunched <= 20){
			Launch (secondaryAngle, 700, 0, 1);
			Launch (secondaryAngle + 5, 700, -.3F, 1);
			Launch (secondaryAngle - 5, 700, .3F, 1);
		}
		if (secondaryAngle >= 140 || secondaryAngle <= 40) {
			secondaryAngleIncreasing = !secondaryAngleIncreasing;
		}
		if (secondaryAngleIncreasing)
			secondaryAngle += 5;
		else
			secondaryAngle -= 5;
	}

	private void Launch(float angle, float power, float xStart, float yStart) {
		Vector3 pos = new Vector3 (xStart, yStart, 0);
		GameObject splash = Instantiate (sprinklerSplash, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
		splash.SetActive (true);
		Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
		Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
		splashrb.AddForce(dir*power);
	}

	IEnumerator WaitToEnd(float time) {
		yield return new WaitForSeconds(time);
		splashManagerScript.DestroySplashes ();
		controlScript.SwitchPlayerControl ();
	}

}
