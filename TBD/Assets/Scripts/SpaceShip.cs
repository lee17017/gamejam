using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpaceShip : NetworkBehaviour {
    
    public int numberOfPlayers;
    private bool waiting;
    public Player player;
    public Action[] actions;
    public Camera[] cams;

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        waiting = numberOfPlayers < 3;
        if (waiting)
        {
            return;
        }

	}

    [Command]
    public void CmdSetNumber()
    {
        numberOfPlayers++;
        RpcSetNumber(numberOfPlayers);
    }

    [ClientRpc]
    public void RpcSetNumber(int n)
    {
        numberOfPlayers = n;
        if (player != null && player.isLocalPlayer)
        {
            player.state = numberOfPlayers - 1;
            cams[player.state].enabled = true;
        }
    }

    void NumChange(int n)
    {
        numberOfPlayers = n;
    }

    [Command]
    public void CmdSetState()
    {
        player.RpcSetState(numberOfPlayers);
    }

    public void Register()
    {
        CmdSetNumber();
    }
}
