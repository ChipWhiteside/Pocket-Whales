using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * 750*(Time.deltaTime));
	}
}
