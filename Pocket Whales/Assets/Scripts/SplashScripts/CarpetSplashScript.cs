using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetSplashScript : MonoBehaviour, SplashInterface {

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
	 * Controller Game Object
	 */
	public GameObject control;

	/*
	 * Is the turn already ending
	 */
	private bool endingTurn;


	// Use this for initialization
	void Start () {
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

	public void EffectOnHit () {

	}

	public void EffectOnTap () {

	}

	public void EffectOnBounce () {

	}

	public void EndTurn() {

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
		Destroy (gameObject);
		DestroyAllObjects ();
		controlScript.SwitchPlayerControl ();
	}

}
