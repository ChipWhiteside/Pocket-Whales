using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardWins : MonoBehaviour
{

    /*
	 * game object for user entry of username
	 */
    private string username = GameManager.username;

    public Text lead;

    void Start()
    {
        lead = GameObject.Find("leaderboard").GetComponent<Text>();
        StartCoroutine(setLeaderboard());
    }

    private void Update()
    {

    }


    public IEnumerator setLeaderboard()
    {
        WWWForm form = new WWWForm();
        form.AddField("category", "wins");
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getLeaderboard.php", form);
        yield return www;
        string reply = www.text;
        Debug.Log(reply);
        lead.text = reply;
        string[] toPrint = reply.Split('^');
        lead.text = "Rank\t\t\t\t\t\t\t\tUsername\t\t\t\t\t\t\t\tLevel\t\t\t\t\t\t\t\tWins\n\n";

        for (int i = 0; i < toPrint.Length; i++)
        {
            int rank = i + 1;
            lead.text += rank + toPrint[i] + "\n\n";
        }
    }
}
