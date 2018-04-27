using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {
	/*
	 * regulates the volume
	 */
	public Slider volume = null;
	/*
	 * the source of the music we are using
	 */
	public AudioSource myMusic = null;
	/*
	 * the text that the user will see
	 */
	public Text volumeText = null;

	void Awake(){
		//finds our music object and keeps it alive
		GameObject[] music = GameObject.FindGameObjectsWithTag ("music");
		if (music.Length > 1)
			Destroy (this.gameObject); //destroy any other music 
		DontDestroyOnLoad (this.gameObject); //dont destroy this other one
		volumeText = null;
		volume = null;
		myMusic = null;


	}



	// Update is called once per frame
	void Update () {
		//get the current seen
		Scene current = SceneManager.GetActiveScene ();

		//check if it part of unity
		if (current.name == "Settings") {
			//bassically find all sliders 
			Slider[] sliders = Slider.FindObjectsOfType (typeof (Slider)) as Slider[];
			//get the volume slider
			volume = sliders [1];
			//find the object for the movie scene
			myMusic = FindObjectOfType (typeof (AudioSource)) as AudioSource;

			//get all the texts in this scene
			Text[] texts = Text.FindObjectsOfType (typeof (Text)) as Text[];
			//identify the one we want
			volumeText = texts [1];
			//adjust accordingly
			myMusic.volume = volume.value;
			volumeText.text = "Volume: " + ((int)(volume.value * 100)).ToString();
		}
	
	}
}
