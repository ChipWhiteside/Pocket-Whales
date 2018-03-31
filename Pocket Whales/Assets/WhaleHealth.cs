using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleHealth : MonoBehaviour {
	 
	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoseHealth (int amount) 
	{
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0) {
			
		}
	}
}
