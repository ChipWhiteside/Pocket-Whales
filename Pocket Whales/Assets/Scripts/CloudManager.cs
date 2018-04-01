using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {

	public class Cloud : MonoBehaviour {
		private float speed; //speed cloud will move across screen
		private float scale; //size of the cloud
		private float height; //hight of the cloud
		private Rigidbody2D rb; 

		public float leftBoundary; //left boundary of the clouds where they disappear when they hit it
		public float rightBoundary; //right boundary for the clouds where they are created

		public GameObject cloudRef;

		public Cloud (float speed, float scale, float height) {
			this.speed = speed;
			this.scale = scale;
			this.height = height;
			rb = GetComponent<Rigidbody2D> ();
		}

		void Start() {
			transform.position = new Vector3(rightBoundary, height, 0.0f);
			transform.localScale = new Vector3(scale, scale, scale);
		}

		void Update() {
			while (transform.position.x >= leftBoundary) {
				//Run Loop;
				rb.velocity = new Vector2 ((speed * -1), 0);
				//yield return null;
				//wait one frame
			}
			rb.velocity = new Vector2 (0, 0);
		}
	}

	public int cloudsAllowed; //number of clouds allowed on screen

	private int numClouds; //number of clouds in the game

	// Use this for initialization
	void Start () {
		numClouds = 0;
	}

	/*
	 * Wait: pauses decision for 1 second(s) to make it feel like player reflexes even though its AI
	*/
	IEnumerator waitForTime(float time)
	{
		yield return new WaitForSeconds(time);
	}

	// Update is called once per frame
	void Update () {
		StartCoroutine (waitForTime (7f));
		//yield return new WaitForSeconds(7.0f); //check once every 7 seconds to see if the turn has ended
		if (numClouds < cloudsAllowed) {
			float speed = Random.Range (.7f, 1.2f);
			float scale = Random.Range (.3f, .6f);
			float height = Random.Range (10f, 27f);
			Cloud newCloud = new Cloud (speed, scale, height);
		}
	}




}
