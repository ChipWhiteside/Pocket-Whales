using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour {

    /*
	 * game object for user entry of username
	 */
    private string username = GameManager.username;

    public Text user;

    public void Start()
    {
        user = GameObject.Find("Username").GetComponent<Text>();
    }

    


    public IEnumerator setProfile(string Username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Username);
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getProfile.php", form);
        yield return www;
        string reply = www.text;
        user.text = reply;
    }
}
