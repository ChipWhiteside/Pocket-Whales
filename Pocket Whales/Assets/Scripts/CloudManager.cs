using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

	public float cloudsAllowed; //number of clouds allowed on screen

	private float time; //amount of time we want to wait between cloud spawns
	private float timeStore; 

	public int numClouds; //number of clouds in the game
	private Vector3 cloudStart;

	public GameObject originCloud; //the origional cloud

	void Start () {
		time = Random.Range (4f, 18f);
		numClouds = 0;
		cloudsAllowed = Random.Range (7f, 20f);
		print ("Clouds Allowed: " + cloudsAllowed);
		timeStore = time;
		cloudStart = new Vector3(60f, Random.Range (10f, 27f), 0f);

		createCloud ();
		createCloud ();
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

		if (time > 0) {
			time -= (Time.deltaTime);
		} else {
			time = timeStore;
			//print ("In else statment");
			//yield return new WaitForSeconds(7.0f); //check once every 7 seconds to see if the turn has ended
			if (numClouds < cloudsAllowed) {
				Vector3 pos = new Vector3 (0, 1, 0);
				GameObject newCloud = Instantiate(originCloud, cloudStart, originCloud.transform.rotation);
				numClouds++;
				CloudScript newCloudScript = newCloud.GetComponent<CloudScript> ();
				newCloudScript.setValues (newCloud.transform.position.x, Random.Range (1f, 5f), Random.Range (.3f, .6f), Random.Range (12f, 30f));
				//Cloud newCloud = new Cloud (speed, scale, height);
			}	
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

	void createCloud () {
		GameObject newCloud = Instantiate(originCloud, new Vector3(Random.Range(-40f, 40f), Random.Range(12f, 30f)), originCloud.transform.rotation);
		numClouds++;
		CloudScript newCloudScript = newCloud.GetComponent<CloudScript> ();
		newCloudScript.setValues (newCloud.transform.position.x, Random.Range (1f, 5f), Random.Range (.3f, .6f), Random.Range (12f, 30f));
	}
}
