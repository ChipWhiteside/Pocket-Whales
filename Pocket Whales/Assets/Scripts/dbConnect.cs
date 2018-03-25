using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dbConnect : MonoBehaviour
{

	public string[] items;
	// Use this for initialization
	IEnumerator Start ()
	{
		WWW itemData = new WWW ("http://localhost/pocketwhalesDB.php");
		yield return itemData;
		string itemsDataString = itemData.text;
		print (itemsDataString);

		items = itemsDataString.Split (';');  
		print (getData (items [0], "value:"));
	}

	string getData (string data, string index)
	{
		int startPos = data.IndexOf(index) + index.Length;
		string value = data.Substring (startPos);
		if(value.Contains("|"))
			value = value.Remove(value.IndexOf("|"));
		return value;
	}
}
