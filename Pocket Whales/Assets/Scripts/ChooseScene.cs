using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
