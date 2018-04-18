using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileScript : MonoBehaviour {

    /*
	 * game object for user entry of username
	 */
    private string username = GameManager.username;

    public Text prof;

    void Start()
    {
        prof = GameObject.Find("Profile").GetComponent<Text>();
        StartCoroutine(setProfile(username));
    }

    private void Update()
    {
       
    }


    public IEnumerator setProfile(string Username)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Username);
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getProfile.php", form);
        yield return www;
        string reply = www.text;
        string[] toPrint = reply.Split(new char[0]);
        int i = 0;
        while (i < toPrint.Length)
        {
            prof.text += toPrint[i] + "\n\n";
            i++;
        }
    }
}
