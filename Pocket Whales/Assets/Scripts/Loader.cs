using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	public GameObject gameManager;
	// Use this for initialization
	void Awake () {
		StartCoroutine (LoadUp ());
	}
	
	public IEnumerator LoadUp(){
		yield return new WaitForSeconds (5);
		if (GameManager.instance == null)
			Instantiate (gameManager);
	}
}
