using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Login : MonoBehaviour {
	
	/*
	 * This will hold the user name that the player inputs
	 * 
	 */
	public GameObject username;
	/*
	 * Holds the password from input
	 * 
	 * 
	 */
	public GameObject password;
	/*
	 * Wil hold the string versions of player input
	 * 
	 */
	private string Username;
	/*
	 * String version of password
	 * 
	 */
	private string Password;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// convert cgame object to string and save it


		/*if (Input.GetKeyDown (KeyCode.L)) {
			if(Username != "" && Password != "")
				 StartCoroutine (login (Username, Password));
		}*/
	}

	/**
	 * 
	 * Makes sure that user has filled all fields
	 * 
	 */
	void checkEntries(){
		if (Username != "" && Password != "") {
			Username = username.GetComponent<InputField> ().text;
			Password = password.GetComponent<InputField> ().text;

			StartCoroutine (login (Username, Password));

		}
		else
			Debug.Log ("No info");
	}
	/**
	 * This connects to server and checks validity of user entry
	 * 
	 * @param username string
	 * @param password string
	 * @return bassically wait until coroutine is complete
	 */

	IEnumerator login(string username, string password){
		//bassically a way to post safely to a server
		WWWForm form = new WWWForm();
		form.AddField ("username", username);
		form.AddField ("password", password);
		//open a connection to our server, this is not very robust but it helps with tests
		WWW www = new WWW ("https://csweb.wheaton.edu/~pocketwhales/Login.php", form);
		//wait until server has sent everything to unity
		yield return www;

		//what does the server say?
		string reply = www.text;
		Debug.Log (reply);
		if (reply == "gooooooood!!") {
			Debug.Log ("in ");
			GameManager.instance.username = Username;

		}
	}
}
