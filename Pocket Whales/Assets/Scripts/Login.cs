using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Login : MonoBehaviour {

	public GameObject username;
	public GameObject password;

	private string Username;
	private string Password;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Username = username.GetComponent<InputField> ().text;
		Password = password.GetComponent<InputField> ().text;

		/*if (Input.GetKeyDown (KeyCode.L)) {
			if(Username != "" && Password != "")
				 StartCoroutine (login (Username, Password));
		}*/
	}
	void checkEntries(){
		if (Username != "" && Password != "")
			StartCoroutine (login (Username, Password));
		else
			Debug.Log ("No info");
	}
	IEnumerator login(string username, string password){
		WWWForm form = new WWWForm();
		form.AddField ("username", username);
		form.AddField ("password", password);
		WWW www = new WWW ("http://localhost/Login.php", form);
		yield return www;
		Debug.Log (www.text);
	}
}
