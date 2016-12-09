using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public SpaceShip ship;
    public int state = 0;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            ship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>();
            ship.player = this;
            ship.Register();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        ship.actions[state].Move();
	}

    [ClientRpc]
    public void RpcSetState(int n)
    {
        if (isLocalPlayer)
        {
            state = n;
            ship.cams[n].enabled=true;
        }
    }
}
