using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCircle : MonoBehaviour {

	public GameObject whale;

	public GameObject whale2;

	private GameObject currentWhale;

	private Vector3 screenPoint;

	private Vector3 offset;

	private bool turnStarted;

	public Text angleText;

	public Text powerText;

	public GameObject splash;

	private PlayerController playerController;

	public bool isCompGame;

	public GameObject camera;

	private CameraControl cameraControl;

	public Camera playerCam;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public int lengthOfLineRenderer = 20;

	// Use this for initialization
	void Start () {
		currentWhale = whale;
		turnStarted = false;
		transform.position = currentWhale.transform.position;
		lengthOfLineRenderer = 20;
		cameraControl = camera.GetComponent<CameraControl> ();

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.widthMultiplier = 0.2f;
		lineRenderer.positionCount = lengthOfLineRenderer;

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;
	}
	
	// Update is called once per frame
	void Update () {
		if(!turnStarted)
			transform.position = currentWhale.transform.position;
	}

	void OnMouseDown()
	{
		turnStarted = true;
		screenPoint = playerCam.WorldToScreenPoint (transform.position);
		offset = transform.position - playerCam.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = playerCam.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		UpdatePowerAngleText ();
		PlotTrajectory ();
		cameraControl.setFieldOfView (FindPower ());
	}

	void OnMouseUp() {
		float power = FindPower ();
		float angle = FindAngle ();
		playerController = currentWhale.GetComponent<PlayerController> ();
		playerController.Launch (angle, power);
	}

	void UpdatePowerAngleText() {
		int power = (int) FindPower ();
		int angle = (int) FindAngle ();
		angleText.text = angle.ToString() + "°";
		powerText.text = power.ToString () + "%";
	}

	float FindAngle (){
		float whaleX = currentWhale.transform.position.x;
		float whaleY = currentWhale.transform.position.y;
		float circleX = transform.position.x;
		float circleY = transform.position.y;
		float rise = whaleY - circleY;
		float run = whaleX - circleX;
		return ((Mathf.Atan2 (rise, run)) * Mathf.Rad2Deg);
	}

	float FindPower (){
		Vector3 whaleScreenPosition = playerCam.WorldToScreenPoint (currentWhale.transform.position);
		Vector3 circleScreenPosition = playerCam.WorldToScreenPoint (transform.position);
		float whaleX = whaleScreenPosition.x;
		float whaleY = whaleScreenPosition.y;
		float circleX = circleScreenPosition.x;
		float circleY = circleScreenPosition.y;
		float x = Mathf.Pow (whaleX - circleX, 2F);
		float y = Mathf.Pow (whaleY - circleY, 2F);
		return Mathf.Sqrt (x + y) / 5;

	}

	public void SwitchTurn() {
		currentWhale.GetComponent<Rigidbody2D> ().isKinematic = false;
		currentWhale.GetComponent<Collider2D> ().enabled = true;
		currentWhale.GetComponent<Rigidbody2D> ().freezeRotation = false;
		transform.position = whale.transform.position;
		if (isCompGame) {
			if (gameObject.activeSelf) {
				gameObject.SetActive (false);
				print ("deactivate");
			} else {
				gameObject.SetActive (true);
				print ("activate");
			}
		}
		turnStarted = false;
		SwitchWhale ();
	}

	void SwitchWhale() {
			if (currentWhale == whale)
				currentWhale = whale2;
			else if (currentWhale == whale2)
				currentWhale = whale;
	}

	public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
		return start + startVelocity*time + Physics.gravity*time*time*0.5f;
	}
		
	public void PlotTrajectory() {
		Vector3 dir = Quaternion.AngleAxis(FindAngle(), Vector3.forward) * Vector3.right;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		var points = new Vector3[lengthOfLineRenderer];
		float j = 0;
		for (int i = 0; i < lengthOfLineRenderer; i++)
		{
			points [i] = PlotTrajectoryAtTime (whale.transform.position, dir * (FindPower ()), j);
			j += .03f;
		}
		lineRenderer.SetPositions(points);
	}

}
