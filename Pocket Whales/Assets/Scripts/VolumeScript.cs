using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour {

    public Slider volume;
    public AudioSource myMusic;
    public Text volumeText;
	
	// Update is called once per frame
	void Update () {
        myMusic.volume = volume.value;
        volumeText.text = "Volume: " + ((int)(volume.value * 100)).ToString();
	}
}
