using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {
	public Scene currentScene;
	public static string username;
	public static GameManager instance = null;
	// Use this for initialization

	void Awake(){

		username = "";
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			DestroyObject (gameObject);

		}
		DontDestroyOnLoad (gameObject);
		MainMenu();
	}

	
	// Update is called once per frame
	void Update () {
		
		currentScene = SceneManager.GetActiveScene ();
		if (currentScene.name == "MainMenu" && username != "") {
			Text welcomeText = GameObject.Find ("HelloText").GetComponent<Text> ();
			welcomeText.text = "Hello " + username + "!";

		}
	
		
	}

	public void MainMenu(){

		SceneManager.LoadScene ("MainMenu");
	}
	public void Settings () {
		SceneManager.LoadScene("Settings");
	}

	public void SinglePlayer () {
		SceneManager.LoadScene("CompGame");
	}

	public void Multiplayer () {
		SceneManager.LoadScene("Multiplayer");
	}
	public void Profile(){
		SceneManager.LoadScene ("Login");
	}
	public void Register(){
		SceneManager.LoadScene ("Register");
	}
	public void SinglePlayerMenu(){
		SceneManager.LoadScene ("SinglePlayerMenu");
	}
	public void backToSettings()
	{
		SceneManager.LoadScene("Settings");
	}
	public void About(){
		SceneManager.LoadScene ("AboutGame");
	}
    public void viewProfile()
    {
        SceneManager.LoadScene("ViewProfile");
    }
}
