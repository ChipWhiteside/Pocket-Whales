﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinklerSplashScript : MonoBehaviour, SplashInterface {

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


	// Use this for initialization
	void Start () {
		energyEffect = 1;
		maxActiveTime = 7;
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
		EndTurn ();
	}

	public void EffectOnBounce() {

	}

	public void EffectOnTap() {

	}

	public void EndTurn() {
		if (!endingTurn) {
			endingTurn = true;
			splashManagerScript.RemoveFromSplashes (gameObject);
			Destroy (gameObject);
		}
	}

}