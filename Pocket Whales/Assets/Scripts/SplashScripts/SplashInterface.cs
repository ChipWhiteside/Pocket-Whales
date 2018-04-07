using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SplashInterface {

	void Start ();
	
	void Update ();

	void OnCollisionEnter2D ();

	/**
	 * The effect on the splash when you launch
	 */
	void EffectOnLaunch();

	/**
	 * The effect on the splash after an amount of time
	 */
	void EffectOnTime();

	/**
	 * The effect on the splash after it hits the whale
	 */
	void EffectOnHit();

	/**
	 * The effect on the splash when the screen is tapped again
	 */ 
	void EffectOnTap ();

	/**
	 * The effect on the splash after it hits the ground
	 */
	void EffectOnBounce();

	/**
	 * Ends the current players turn and activates the other whale
	 */
	void EndTurn();
}
