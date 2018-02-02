using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Rotate the object around its local X axis at 1 degree per second
		transform.Rotate(Vector3.back * 100*(Time.deltaTime));

		// ...also rotate around the World's Y axis
		//transform.Rotate(Vector3.up * (20 * Time.deltaTime), Space.World);
	}
}
