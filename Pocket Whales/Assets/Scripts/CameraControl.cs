using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Camera mainCamera;

	public Camera playerCamera;

	private bool camSwitch;

	public GameObject whale1;

	public GameObject whale2;

	private GameObject currentWhale;

	private Vector3 startingOffset;

	private Vector3 offset;

	public GameObject splashManager;

	// Use this for initialization
	void Start () {
		camSwitch = false;
		currentWhale = whale1;
		startingOffset = playerCamera.transform.position - currentWhale.transform.position;
		offset = playerCamera.transform.position - currentWhale.transform.position;
		mainCamera.gameObject.SetActive (camSwitch);
		playerCamera.gameObject.SetActive (!camSwitch);
	}
	
	// Update is called once per frame
	void Update () {
		playerCamera.transform.position = currentWhale.transform.position + offset;
	}

	public void SwitchWhale(){
		if (currentWhale.Equals (whale1))
			currentWhale = whale2;
		else
			currentWhale = whale1;
	}

	public void SwitchCamera() {
		camSwitch = !camSwitch;
		mainCamera.gameObject.SetActive (camSwitch);
		playerCamera.gameObject.SetActive (!camSwitch);
	}

	public void setFieldOfView(float power){ //change field of view for playerCam
		playerCamera.orthographicSize = power * 0.3f + 10;
	}

	public void FollowSplash (){

	}

}
