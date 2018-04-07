using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour {

	public GameObject player1; //the player controlled whale

	public GameObject player2; //the AI whale

	public bool canMove; //is the current whale able to move

	public int turn; //keeps track of who's turn it is

	public bool looping;

	public int moveTime; //how long the whale can move for

	public float moveSpeed; //how fast the whale can move

	public bool isCompGame; //if this is a player vs. comp game

	public bool playerHit;

	public GameObject dragCircle;

	private DragCircle dCircleScript;

	public Text endGameText;

	public Button endGameButton;

	public GameObject endGame;

	void Start()
	{
		canMove = true;
		playerHit = false;
		looping = false;
		turn = 1;
		dCircleScript = dragCircle.GetComponent<DragCircle> ();
	}
		
	/*
	 * id: 0 for a multiplayer game (player vs player) 1 for a computer game (player vs computer) 
	*/
	public void SwitchPlayerControl() 
	{
		//print("Turn: " + turn);
		if (turn == 1 | turn == 11) {
			if (isCompGame)
				player2.GetComponent<SmartCompController> ().enabled = true;
			else {
				player2.GetComponent<PlayerController> ().enabled = true;
			}
			player1.GetComponent<PlayerController> ().enabled = false;
			turn = 2;
		} else if (turn == 2 | turn == 22) {
			if (isCompGame) {
				player2.GetComponent<SmartCompController> ().enabled = false;
			} else {
				player2.GetComponent<PlayerController> ().enabled = false;
			}
			player1.GetComponent<PlayerController> ().enabled = true;
			turn = 1;

		} else
			return; //end the game
		dCircleScript.SwitchTurn ();
		canMove = true;
		//print("Turn After: " + turn);
	}

	/*
	 * No player can move until SwitchPlayerControl is called again
	 */
	public void TakeControl(int player) {
		if (turn == 1)
			turn = 11; //arbitrary values to remember which players turn was last but keep control away from players
		else if (turn == 2)
			turn = 22;
		else
			turn = 3;
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

	public void EndGame(string winnerName) {
		TakeControl (3);
		endGameText.text = "Congratulations " + winnerName + "!";
		endGame.SetActive (true);
	}


}
