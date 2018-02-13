using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSplash : MonoBehaviour {

	private Rigidbody2D rb;


	// Use this for initialization
	public void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.IsSleeping()) {
			Destroy (gameObject);
		}
	}

	public void Launch() {
		
	}

	void FixedUpdate ()
	{
		
	}
}
