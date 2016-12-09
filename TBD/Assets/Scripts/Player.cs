using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public Camera[] cams;
    private int numberOfPlayers;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            Debug.Log(5);
            CmdConnection();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdConnection()
    {
        RpcCameraChange(numberOfPlayers%2);
        numberOfPlayers++;
    }

    [ClientRpc]
    public void RpcCameraChange(int i)
    {
        if (isLocalPlayer)
        {
            Debug.Log(6);
            cams[i].enabled = true;
        }
    }
}
