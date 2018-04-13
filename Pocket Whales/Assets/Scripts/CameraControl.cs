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

	private Vector3 targetPos;

	private float waitTime = 2.0f; //amount of time the game will show the whole map at beginning of match

	public Transform startMarker; //position of the main camera before moving
	public Transform endMarker; //targer position for main camera to be after moving
	private float speed = 15.0f; //speed of the camera movement
	private float startTime;
	private float journeyLength;

	private float cameraSizeStart = 30.0f;
	private float cameraSizeEnd = 10.0f;

	public float elapsed = 0.0f;
	public float duration = 5.0f;

	// Use this for initialization
	void Start () {
		startTime = Time.time + waitTime;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

		mainCamera.orthographic = true;
		camSwitch = true;
		zooming = true;
		currentWhale = whale1;
		//startingOffset = playerCamera.transform.position - currentWhale.transform.position;
		offset = playerCamera.transform.position - currentWhale.transform.position;
		ZoomInAtStart ();

	}


	// Update is called once per frame
	void Update () {
		if (zooming) {
			waitTime -= Time.deltaTime * 1.0f;
			if (waitTime <= 0) {
				float distCovered = (Time.time - startTime) * speed; //how much distance has been covered
				float fracJourney = distCovered / journeyLength; //starts at 0 and grows until it reaches destination where it = 1
				float camSize = 10.0f + ((1 - fracJourney) * 20.0f); //starts at 30, ends at 10; uses fracJourney as a percentage of 20 to add to 10 to use as the camera size
				Camera.main.transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney); //sets the camera position
				Camera.main.orthographicSize = camSize; //sets camera size
				print (fracJourney);
				if (fracJourney >= 1) {
					print ("In if statment");
					zooming = false;
					SwitchCamera ();
				}
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

	public void ZoomInAtStart() {
		startPos = mainCamera.transform.position;
		targetPos = currentWhale.transform.position + offset;
	}

	public void FollowSplash (){

	}

}
