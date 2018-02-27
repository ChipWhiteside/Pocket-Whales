using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour {

<<<<<<< HEAD
	public GameObject player1; //the player controlled whale

	public GameObject player2; //the AI whale

	public bool canMove; //is the current whale able to move

	public int turn; //keeps track of who's turn it is

	public bool looping;

	public int moveTime; //how long the whale can move for

	public float moveSpeed; //how fast the whale can move
=======
	public GameObject player1;

	public GameObject player2;

	public bool canMove;

	public int turn;

	public bool looping;

	public int moveTime;

	public float moveSpeed;
>>>>>>> d6aa0420e0a9d38a972633de2929c22739447253

	void Start()
	{
		canMove = true;
		looping = false;
	}
		
	public void SwitchPlayerControl() 
	{
		print("Turn: " + turn);
		if (turn == 1) {
			player1.GetComponent<PlayerController> ().enabled = false;
			turn = 2;
			player2.GetComponent<PlayerController> ().enabled = true;
		} else if (turn == 2) {
			player2.GetComponent<PlayerController> ().enabled = false;
			turn = 1;
			player1.GetComponent<PlayerController> ().enabled = true;
		}
		canMove = true;
		print("Turn After: " + turn);
	}

	public void MoveR(Rigidbody2D rb) 
	{
		StartCoroutine (MoveHelper(moveTime, rb, 1));
	}

	public void MoveL(Rigidbody2D rb) 
	{
		StartCoroutine (MoveHelper(moveTime, rb, -1));
	}

	IEnumerator MoveHelper(int time, Rigidbody2D rb, int direction) {
		float i = 0;
		looping = true;
		canMove = false;
		while (i <= time) {
			//Run Loop;
			rb.velocity = new Vector2 ((moveSpeed * direction), 0);
			i += Time.deltaTime;
			//add 1 frame's length to i.
			yield return null;
			//wait one frame
		}
		looping = false;
		rb.velocity = new Vector2 (0, 0);
	}
}
