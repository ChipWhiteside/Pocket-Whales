using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

	public float cloudsAllowed; //number of clouds allowed on screen
	public float bCloudsAllowed; //number of background clouds allowed on screen

	private float time; //amount of time we want to wait between cloud spawns
	private float timeStore; 

	public int numClouds; //number of clouds in the game
	public int numBClouds; //number of background clouds on screen
	private Vector3 cloudStart;
	private Vector3 bCloudStart;

	public GameObject originCloud; //the origional cloud
	public GameObject backgroundOCloud; //the origional background cloud

	void Start () {
		time = Random.Range (4f, 18f);
		numClouds = 0;
		cloudsAllowed = Random.Range (7f, 20f);
		bCloudsAllowed = Random.Range (13f, 25f);

		timeStore = time;
		cloudStart = new Vector3(originCloud.transform.position.x, originCloud.transform.position.y, originCloud.transform.position.z);
		bCloudStart = new Vector3(backgroundOCloud.transform.position.x, backgroundOCloud.transform.position.y, backgroundOCloud.transform.position.z);

		int numCloudStart = (int) Random.Range (2f, 4f); //number of foregrounds clouds to be spawned at start
		int numBCloudStart = (int) Random.Range (5f, 7f); //number of background clouds to be spawned at start

		while (numCloudStart > 0) { //spawn a random number of foreground clouds at start of game
			spawnCloud (Random.Range (-40f, 40f));
			numCloudStart--;
		}
		while (numBCloudStart > 0) { //spawn random number of background clouds at start of game
			spawnBCloud (Random.Range (-40f, 40f));
			numBCloudStart--;
		}
	}

	/*
	 * Wait: pauses decision for 1 second(s) to make it feel like player reflexes even though its AI
	*/
	IEnumerator waitForTime(float time)
	{
		yield return new WaitForSeconds(time);
	}

	void Update () {
		originCloud.transform.position = cloudStart;
		originCloud.transform.localScale = new Vector3(.4f, .4f, .4f);;
		backgroundOCloud.transform.position = bCloudStart;
		backgroundOCloud.transform.localScale = new Vector3(.2f, .2f, .2f);

		if (time > 0) {
			time -= (Time.deltaTime);
		} else {
			time = timeStore;
			//print ("In else statment");
			//yield return new WaitForSeconds(7.0f); //check once every 7 seconds to see if the turn has ended
			if (numClouds < cloudsAllowed) {
				spawnCloud (originCloud.transform.position.x);
			}	
			if (numBClouds < bCloudsAllowed) {
				spawnBCloud (backgroundOCloud.transform.position.x);
			}
		}
	}

	void spawnCloud (float xVal) {
		GameObject newCloud = Instantiate(originCloud, new Vector3(xVal, Random.Range(8.5f, 30f)), originCloud.transform.rotation);
		numClouds++;
		CloudScript newCloudScript = newCloud.GetComponent<CloudScript> ();
		//newCloud (Sorting Layer: Piece) (Order in Layer: 1)
		newCloudScript.setValues (newCloud.transform.position.x, Random.Range (2f, 5f), Random.Range (.45f, .6f), newCloud.transform.position.y);
	}

	void spawnBCloud(float xVal) {
		GameObject newBCloud = Instantiate(backgroundOCloud, new Vector3(xVal, Random.Range(8.5f, 30f)), backgroundOCloud.transform.rotation);
		numBClouds++;
		CloudScript newBCloudScript = newBCloud.GetComponent<CloudScript> ();
		newBCloudScript.setValues (newBCloud.transform.position.x, Random.Range (.5f, 1.5f), Random.Range (.15f, .25f), newBCloud.transform.position.y);
	}
}
