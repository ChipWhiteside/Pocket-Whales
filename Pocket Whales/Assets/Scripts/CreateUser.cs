using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour {
	public GameObject username ;
	public GameObject password ;
	public GameObject confirmPassword;

	private string Username;
	private string Password;
	private string ConfirmPassword;

	// Use this for initialization
	void Start () {
		
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
				createUser(Username, Password);
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
		WWW www = new WWW ("http://localhost/makeUser.php", form);
		yield return www;
		Debug.Log (www.text);
	}
}
