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

	private PlayerController playerController;

	public bool isCompGame;

	// Use this for initialization
	void Start () {
		currentWhale = whale;
		turnStarted = false;
		transform.position = currentWhale.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!turnStarted)
			transform.position = currentWhale.transform.position;
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
		UpdatePowerAngleText ();
	}

	void OnMouseUp() {
		float power = FindPower () * 100;
		float angle = FindAngle ();
		playerController = currentWhale.GetComponent<PlayerController> ();
		playerController.Launch (angle, power);
	}

	void UpdatePowerAngleText() {
		float power = FindPower ();
		float angle = FindAngle ();
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
		float whaleX = currentWhale.transform.position.x;
		float whaleY = currentWhale.transform.position.y;
		float circleX = transform.position.x;
		float circleY = transform.position.y;
		float x = Mathf.Pow (whaleX - circleX, 2F);
		float y = Mathf.Pow (whaleY - circleY, 2F);
		return Mathf.Sqrt (x + y);

	}

	public void SwitchTurn() {
		turnStarted = false;
		transform.position = whale.transform.position;
		SwitchWhale ();
	}

	void SwitchWhale() {
		if (currentWhale == whale)
			currentWhale = whale2;
		else if (currentWhale == whale2)
			currentWhale = whale;
	}


}
