using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

	public int cloudsAllowed; //number of clouds allowed on screen

	public float time; //amount of time we want to wait between cloud spawns
	private float timeStore; 

	private int numClouds; //number of clouds in the game

	public GameObject originCloud; //the origional cloud

	void Start () {
		numClouds = 0;
		timeStore = time;
	}

	/*
	 * Wait: pauses decision for 1 second(s) to make it feel like player reflexes even though its AI
	*/
	IEnumerator waitForTime(float time)
	{
		yield return new WaitForSeconds(time);
	}

	void Update () {
		if (time > 0) {
			time -= Time.deltaTime;
		} else {
			print ("Kaitie is the cutest person eva");
			//yield return new WaitForSeconds(7.0f); //check once every 7 seconds to see if the turn has ended
			if (numClouds < cloudsAllowed) {
				GameObject newCloud = Instantiate (originCloud);
				Cloud newCloudScript = GameObject.Find("newCloud").GetComponent<Cloud>();
				float speed = Random.Range (.7f, 1.2f);
				float scale = Random.Range (.3f, .6f);
				float height = Random.Range (10f, 27f);
				newCloudScript.setValues (speed, scale, height);
				//Cloud newCloud = new Cloud (speed, scale, height);
			}	
			time = timeStore;
		}

		/*
		//StartCoroutine (waitForTime (Random.Range(4f, 10f)));
		yield return new WaitForSeconds(10f);
		print ("waiting for time");
		//yield return new WaitForSeconds(7.0f); //check once every 7 seconds to see if the turn has ended
		if (numClouds < cloudsAllowed) {
			GameObject newCloud = Instantiate (originCloud);
			Cloud newCloudScript = GameObject.Find("newCloud").GetComponent<Cloud>();
			float speed = Random.Range (.7f, 1.2f);
			float scale = Random.Range (.3f, .6f);
			float height = Random.Range (10f, 27f);
			newCloudScript.setValues (speed, scale, height);
			//Cloud newCloud = new Cloud (speed, scale, height);
		}
		*/
	}
}
