using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Action : NetworkBehaviour {

    public Player player;

    public virtual void Move()
    {
        return;
    }

    public virtual void reset()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");        
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].GetComponent<Player>().isLocalPlayer)
            {
                player = players[i].GetComponent<Player>();
            }
        }
        return;
    }

}
