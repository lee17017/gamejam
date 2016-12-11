using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ready : MonoBehaviour {

    public Player player;
    private bool clicked = false;
    private Text text;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        text.text = "Klick for Ready";
        StartCoroutine("search");
    }
    private IEnumerator search()
    {
        yield return new WaitForSeconds(1f);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[players.Length-1].GetComponent<Player>();
        if (player != null)
            Debug.Log("FUND");
    }
    public void ready()
    {
        if (!clicked)
        {
            player.CmdUpdateCount(1);
            clicked = true;
            text.text = "Ready";
        }

        else
        {
            player.CmdUpdateCount(-1);
            clicked = false;
            text.text = "Klick for Ready";
        }
    }
}
