using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * if this idea causes bugs and crashes we might be able to just use GetObjects by tags but I feel like that would be slower
 */
public class SplashManagerScript : MonoBehaviour {

	private List<GameObject> allSplashes;
	public GameObject[] splashOptions;
	public Dropdown dropdown;

	// Use this for initialization
	void Start () {
		allSplashes = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	 * This splash will be set to ignore all the other splashes
	 */
	public void IgnoreSplashes(GameObject splash){
		foreach(GameObject o in allSplashes)
			Physics2D.IgnoreCollision(splash.GetComponent<Collider2D>(), o.GetComponent<Collider2D>());
	}

	public void AddToSplashes(GameObject splash){
		allSplashes.Add (splash);
	}

	public void RemoveFromSplashes (GameObject splash){
		allSplashes.Remove (splash);
	}

	public void DestroySplashes() {
		print ("Start destroying");
		foreach (GameObject o in allSplashes) {
			print ("Destroy " + o.name);
			if(o != null)
				Destroy (o);
		}
		allSplashes.Clear ();
	}

	public bool IsEmpty() {
		return allSplashes.Count == 0;
	}
}