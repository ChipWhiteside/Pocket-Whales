using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour {

	public GameObject player1; //the player controlled whale

	public GameObject player2; //the AI whale

	public bool canMove; //is the current whale able to move

	public int turn; //keeps track of who's turn it is

	public bool looping;

	public int moveTime; //how long the whale can move for

	public float moveSpeed; //how fast the whale can move

	public bool isCompGame; //if this is a player vs. comp game

	void Start()
	{
		canMove = true;
		looping = false;
		turn = 1;
	}
		
	/*
	 * id: 0 for a multiplayer game (player vs player) 1 for a computer game (player vs computer) 
	*/
	public void SwitchPlayerControl() 
	{
		print("Turn: " + turn);
		if (turn == 1) {
			player1.GetComponent<PlayerController> ().enabled = false;
			turn = 2;
			if (isCompGame)
				player2.GetComponent<CompController> ().enabled = true;
			else
				player2.GetComponent<PlayerController> ().enabled = true;
		} else if (turn == 2) {
			if (isCompGame)
				player2.GetComponent<CompController> ().enabled = false;
			else
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
