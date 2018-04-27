using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    public Text lead;

    public Button wins;

    public Button losses;

    public Button games;

    void Start()
    {
        Button w = wins.GetComponent<Button>();
        //w.onClick.AddListener(startsetWins);

        Button l = wins.GetComponent<Button>();
        //l.onClick.AddListener(setLosses);

        Button g = wins.GetComponent<Button>();
        //g.onClick.AddListener(setGames);
    }

    void Update()
    {

    }


    public IEnumerator setWins()
    {
        lead = GameObject.Find("Leaderboard").GetComponent<Text>();

        WWWForm form = new WWWForm();
        //form.AddField("category", sort);
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getLeaderboard.php", form);
        yield return www;
        string reply = www.text;
        string[] toPrint = reply.Split(new char[0]);

        lead.text = "Rank\t\tUsername\t\tLevel\t\tWins";

        int i = 0;
        while (i < toPrint.Length)
        {
            lead.text += i + toPrint[i];

            if(i % 3 == 0)
            {
                lead.text += "\n\n";
            }

            i++; 
        }
    }

    public IEnumerator setLosses()
    {
        lead = GameObject.Find("Leaderboard").GetComponent<Text>();

        WWWForm form = new WWWForm();
        //form.AddField("category", sort);
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getLeaderboard.php", form);
        yield return www;
        string reply = www.text;
        string[] toPrint = reply.Split(new char[0]);
        int i = 0;
        while (i < toPrint.Length)
        {
            lead.text += toPrint[i] + "\n\n";
            i++;
        }
    }

    public IEnumerator setGames()
    {
        lead = GameObject.Find("Leaderboard").GetComponent<Text>();

        WWWForm form = new WWWForm();
        //form.AddField("category", sort);
        WWW www = new WWW("https://csweb.wheaton.edu/~pocketwhales/getLeaderboard.php", form);
        yield return www;
        string reply = www.text;
        string[] toPrint = reply.Split(new char[0]);
        int i = 0;
        while (i < toPrint.Length)
        {
            lead.text += toPrint[i] + "\n\n";
            i++;
        }
    }
}
