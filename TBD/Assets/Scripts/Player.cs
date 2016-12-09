using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public Camera[] cams;
    private int numberOfPlayers;

	// Use this for initialization
	void Start () {
        CmdConnection();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdConnection()
    {
        RpcCameraChange(numberOfPlayers);
        numberOfPlayers++;
    }

    [ClientRpc]
    public void RpcCameraChange(int i)
    {
        if (isLocalPlayer)
        {
            cams[i].enabled = true;
        }
    }
}
