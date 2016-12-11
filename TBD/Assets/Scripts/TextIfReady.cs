using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextIfReady : MonoBehaviour {

    public int row;

    private Text text;
    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players.Length);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<Player>().state == row)
            {
                if (players[i].GetComponent<Player>().isLocalPlayer)
                    text.text = "";
                else if (players[i].GetComponent<Player>().ready)
                    text.text = "READY";
                else
                    text.text = "NOT READY";

                return;
            }

        }
        text.text = "NOT HERE";
    }
}
