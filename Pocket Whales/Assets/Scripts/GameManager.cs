﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour {

	public string username;

	// Use this for initialization

	void Awake(){

		username = "";

	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (username != "")
			Debug.Log (username);
		
	}

	public void MainMenu(){

		SceneManager.LoadScene ("MainMenu");
	}
	public void Settings () {
		SceneManager.LoadScene("Settings");
	}

	public void SinglePlayer () {
		SceneManager.LoadScene("SinglePlayer");
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
}