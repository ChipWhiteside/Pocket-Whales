using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

	private float speed = 1f; //speed cloud will move across screen
	private float scale = .4f; //size of the cloud
	private float height = 20f; //hight of the cloud
	private Rigidbody2D rb; 
	private CloudManager cloudMan; //reference to the cloud manager
	private float numClouds;
	private float cloudsAllowed;
	private bool valuesSet = false; 

	public Color color; //the color of the cloud

	public Vector3 userDirection = Vector3.left;

	private Vector3 increaseValues;
	private Vector3 targetPosition;

	public float leftBoundary; //left boundary of the clouds where they disappear when they hit it
	public float rightBoundary; //right boundary for the clouds where they are created

	//public GameObject cloudRef;

	void Start() {
		cloudMan = GameObject.Find("Controller").GetComponent<CloudManager>();
		rb = GetComponent<Rigidbody2D> ();
		numClouds = cloudMan.numClouds;
		cloudsAllowed = cloudMan.cloudsAllowed;
		float colorShade = 1 - (numClouds / cloudsAllowed);
		print ("ColorShade: " + colorShade);
		Renderer rend = GetComponent<Renderer>();
		color = new Color(colorShade, colorShade, colorShade, 1f); //create color based on how many clouds there are
		//rend.material.shader = Color.; //set the color of the cloud
		transform.localScale = new Vector3 (scale, scale, scale);
		increaseValues = new Vector3(0.1f, 0,0);
		targetPosition = new Vector3(-50f, height,0);
	}

	void Update() {

		transform.Translate(userDirection * speed * Time.deltaTime); 
		if (transform.position.x <= leftBoundary) {
			cloudMan.cloudsAllowed++;
			Destroy (this.gameObject);			
		}
	}

	public void setValues(float xVal, float speed, float scale, float height) {
		this.speed = speed;
		this.scale = scale;
		this.height = height;
		transform.position = new Vector3(xVal, height, 0.0f);
		transform.localScale += new Vector3 (scale, scale, scale);
		targetPosition = new Vector3(-60f, height,0);
		valuesSet = true;
	}
}
