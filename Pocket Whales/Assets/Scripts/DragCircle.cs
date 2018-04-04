using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCircle : MonoBehaviour {

	public GameObject whale;

	private Vector3 screenPoint;

	private Vector3 offset;

	private bool turnStarted;

	// Use this for initialization
	void Start () {
		turnStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!turnStarted)
			transform.position = whale.transform.position;
	}

	void OnMouseDown()
	{
		turnStarted = true;
		screenPoint = Camera.main.WorldToScreenPoint (transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}


}
