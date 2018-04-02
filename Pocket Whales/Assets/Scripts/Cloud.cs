using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	private float speed = 100; //speed cloud will move across screen
	private float scale = 1; //size of the cloud
	private float height = 100; //hight of the cloud
	private Rigidbody2D rb; 
	private CloudManager cloudMan; //reference to the cloud manager

	public float leftBoundary; //left boundary of the clouds where they disappear when they hit it
	public float rightBoundary; //right boundary for the clouds where they are created

	//public GameObject cloudRef;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		while (transform.position.x >= leftBoundary) {
			rb.velocity = new Vector2 ((speed * -1), 0);
		}
		rb.velocity = new Vector2 (0, 0);

		if (transform.position.x <= leftBoundary) {
			cloudMan.cloudsAllowed++;
			Destroy (this.gameObject);
		}
	}

	public void setValues(float speed, float scale, float height) {
		this.speed = speed;
		this.scale = scale;
		this.height = height;
		transform.position = new Vector3(rightBoundary, height, 0.0f);
		transform.localScale += new Vector3 (scale, scale, scale);
	}
}
