using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {

	private float speed = 1f; //speed cloud will move across screen
	private float scale = .4f; //size of the cloud
	private CloudManager cloudMan; //reference to the cloud manager
	private float numClouds;
	private float cloudsAllowed;
    private bool isBCloud = false;

	public Color color; //the color of the cloud

	public Vector3 userDirection = Vector3.left;

    private float leftBoundary; //left boundary of the clouds where they disappear when they hit it
	private float rightBoundary; //right boundary for the clouds where they are created

	void Start() {
        leftBoundary = -75f;
        rightBoundary = 75f;
		cloudMan = GameObject.Find("Controller").GetComponent<CloudManager>();
		numClouds = cloudMan.numClouds;
		cloudsAllowed = cloudMan.cloudsAllowed;
		float colorShade = 1 - (numClouds / cloudsAllowed);
		print ("ColorShade: " + colorShade);
		Renderer rend = GetComponent<Renderer>();
		color = new Color(colorShade, colorShade, colorShade, 1f); //create color based on how many clouds there are
		//rend.material.shader = Color.; //set the color of the cloud
		transform.localScale = new Vector3 (scale, scale, scale);
	}

	void Update() {

		transform.Translate(userDirection * speed * Time.deltaTime); 
		if (transform.position.x <= leftBoundary) {
            if (isBCloud)
                cloudMan.numBClouds--;
            else
                cloudMan.numClouds--;
			Destroy (this.gameObject);			
		}
	}

	public void setValues(bool bCloud, float xVal, float speed, float scale, float height) {
        isBCloud = bCloud;
        this.speed = speed;
		this.scale = scale;
		transform.position = new Vector3(xVal, height, 0.0f);
		transform.localScale += new Vector3 (scale, scale, scale);
	}
}
