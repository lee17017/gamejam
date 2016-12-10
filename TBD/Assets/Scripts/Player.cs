using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public SpaceShip ship;
    public Action[] actions;
    public int state;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            ship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>();
            ship.player = this;
            state = GameObject.FindGameObjectsWithTag("Player").Length - 1;
            ship.cams[state].enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdCycle();
        }
        actions[state].Move();
	}

    [ClientRpc]
    public void RpcCycle()
    {
        if (isLocalPlayer)
        {
            state++;
            state = state % 3;
            CycleCams();
        }
    }

    [Command]
    public void CmdCycle()
    {
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().RpcCycle();
        GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<Player>().RpcCycle();
        GameObject.FindGameObjectsWithTag("Player")[2].GetComponent<Player>().RpcCycle();
    }

    private void CycleCams()
    {
        for (int i=0;i<3;i++)
        {
            ship.cams[i].enabled = i==state;
        }
    }
}
