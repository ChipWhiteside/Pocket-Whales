using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour {
	/*
	 * game object for user entry of username
	 */
	public GameObject username ;
	/*
	 * game object for user entry of password
	 */
	public GameObject password ;
	/*
	 * game object for user entry of password to ensure they can remember it
	 */
	public GameObject confirmPassword;
	/*
	 * string version of username
	 */
	private string Username;
	/*
	 * 
	 */
	private string Password;
	/*
	 * 
	 */
	private string ConfirmPassword;

	// Use this for initialization
	void Start () {
		
		Username = null;

		Password = null;

		ConfirmPassword = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		ConfirmPassword = confirmPassword.GetComponent<InputField>().text;

		/*if (Input.GetKeyDown (KeyCode.L)) {
			if(Username != "" && Password != "")
				 StartCoroutine (login (Username, Password));
		}*/
	}
	public void checkEntries(){
		Debug.Log ("IN CHECK");
		if (Username != "" && Password != "" && ConfirmPassword != "") {
			if (Password != ConfirmPassword)
				Debug.Log ("passwords dont match");
			else {
				Debug.Log ("calling Create User");
				StartCoroutine(createUser(Username, Password));
			}
		}
		else
			Debug.Log ("No info");
	}

	public IEnumerator createUser(string Username, string Password){
		Debug.Log ("making user");
		WWWForm form = new WWWForm();
		form.AddField ("username", Username);
		form.AddField ("password", Password);
		WWW www = new WWW ("https://csweb.wheaton.edu/~pocketwhales/makeUser.php", form);
		yield return www;
		string reply = www.text;
		if (reply.Contains("1")) {
			Debug.Log ("in ");
			GameManager.username = Username;

		}
		Debug.Log (www.text);
	}
}
