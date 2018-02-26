using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	private SpriteRenderer sr;

	private Sprite whaleActive;

	private Sprite whaleIdle;

	public float force;

	public GameObject projectile;

	private GameObject controller;

	private ControlScript control;

	public LaunchSplash launchScript;

	public float angle;

	public int playerNo;

	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		whaleActive = Resources.Load<Sprite>("Whale_Active");
		whaleIdle = Resources.Load<Sprite> ("Whale_Idle");
		controller = GameObject.Find("Controller");
		control = controller.GetComponent<ControlScript>();
	}

	void Update ()
	{
		if (Input.GetKeyDown ("space") && playerNo == control.turn && !control.looping) {
			print("space key was pressed");
			Vector3 pos = new Vector3 (0, 1, 0);
			GameObject splash = Instantiate (projectile, transform.position + pos, transform.rotation); //projectile gets same position and rotation as whale
			splash.SetActive (true);
			sr.sprite = whaleActive;
			StartCoroutine(LaunchAnimation());
			Rigidbody2D splashrb = splash.GetComponent<Rigidbody2D> ();
			Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
			splashrb.AddForce(dir*force);
			control.SwitchPlayerControl();
		}
			
	}

	void FixedUpdate ()
	{
		if (Input.GetKeyDown ("right")){
			if (playerNo == control.turn && control.canMove)
				control.MoveR (rb);
		}
		if (Input.GetKeyDown ("left")) {
			if (playerNo == control.turn && control.canMove)
				control.MoveL (rb);
		}
	}

	IEnumerator LaunchAnimation() {
		yield return new WaitForSeconds(0.5f);
		sr.sprite = whaleIdle;
	}

	/*void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
		}
	}*/

}
