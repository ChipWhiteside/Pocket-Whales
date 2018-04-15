using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Camera mainCamera;

	public Camera playerCamera;

	private bool camSwitch;

	public GameObject whale1;

	public GameObject whale2;

	private bool zooming;

	private GameObject currentWhale;

	//private Vector3 startingOffset;

	private Vector3 offset;

	public GameObject splashManager;

	private Vector3 startPos;

	private Vector3 endPos;

	private Vector3 targetPos;

	private float waitTime = 2.0f; //amount of time the game will show the whole map at beginning of match

	//public Transform startMarker; //position of the main camera before moving
	//public Transform endMarker; //target position for main camera to be after moving
	private float speed; //speed of the camera movement
	private float startTime;
	private float journeyLength;

	private float cameraSizeStart = 30.0f;
	private float cameraSizeEnd = 10.0f;
	private float camSizeDif;
	private float zoomToSize;

	public float elapsed = 0.0f;
	public float duration = 5.0f;

	// Use this for initialization
	void Start () {
		startTime = Time.time + waitTime;

		mainCamera.orthographic = true;
		camSwitch = true;
		zooming = false;
		currentWhale = whale1;
		//startingOffset = playerCamera.transform.position - currentWhale.transform.position;
		offset = playerCamera.transform.position - currentWhale.transform.position;
		StartCoroutine (WaitAndCall());
	}


	// Update is called once per frame
	void Update () {
		if (zooming) {
			float distCovered = (Time.time - startTime) * speed; //how much distance has been covered
			float fracJourney = distCovered / journeyLength; //starts at 0 and grows until it reaches destination where it = 1
			float camSize = zoomToSize + ((1 - fracJourney) * camSizeDif); //starts at 30, ends at 10; uses fracJourney as a percentage of 20 to add to 10 to use as the camera size
			Camera.main.transform.position = Vector3.Lerp (startPos, endPos, fracJourney); //sets the camera position
			Camera.main.orthographicSize = camSize; //sets camera size				print (fracJourney);
			if (fracJourney >= 1) {
				print ("In if statment");
				zooming = false;
				//activate dragCircle
				SwitchCamera ();
			}
		}
		else if (!zooming) {
			playerCamera.transform.position = currentWhale.transform.position + offset;
		}
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

	/*
	 * So the coroutine can be clled from another class
	 */
	public void Zoom(Vector3 camEndPos, float camMoveSpeed, float endCamSize, float startCamSize) {
		StartCoroutine (ZoomHelper(camEndPos, camMoveSpeed, endCamSize, startCamSize));
	}

	/*
	 * zooms mainCamera
	 */
	IEnumerator ZoomHelper(Vector3 camEndPos, float camMoveSpeed, float endCamSize, float startCamSize) {
		//deactivate drag circle
		//yield return new WaitForSeconds (2f);
		startPos = mainCamera.transform.position;
		endPos = camEndPos;
		targetPos = camEndPos;
		journeyLength = Vector3.Distance(startPos, endPos);
		speed = camMoveSpeed;
		camSizeDif = startCamSize - endCamSize;
		zoomToSize = endCamSize;
		zooming = true;
		yield return new WaitForSeconds (0f);
	}

	/*
	 * Just so when the game starts the camera wont zoom to the old position before whale fell to ground
	 */
	IEnumerator WaitAndCall(){
		yield return new WaitForSeconds (2f);
		Zoom(currentWhale.transform.position, 15f, 10.0f, 30.0f);
	}
	/*
	 * MainCamera set to playerCam's Position
	 */
	public void AlignCameras() {
		mainCamera.transform.position = playerCamera.transform.position;
	}

	public void FollowSplash (){

	}

}
