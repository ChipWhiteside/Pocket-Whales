using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardGames : MonoBehaviour
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
        form.AddField("category", "games");
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getLeaderboard.php", form);
        yield return www;
        string reply = www.text;
        lead.text = reply;
        string[] toPrint = reply.Split(new char[0]);
        lead.text = "Rank\t\tUsername\t\tLevel\t\tGames";

        for (int i = 0; i < toPrint.Length; i++)
        {
            int rank = i + 1;
            
            lead.text += rank + toPrint[i];

            if (i % 3 == 0)
            {
                lead.text += "\n\n";
            }
        }
    }
}

