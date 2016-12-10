using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    
    public SpaceShip ship;
    public Action[] actions;
    public int state;
    public GameObject bulletPref;
	// Use this for initialization
	void Start () {
        ship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>();
        
        if (isLocalPlayer)
        {
            ship.player = this;
            state = GameObject.FindGameObjectsWithTag("Player").Length - 1;
            ship.cams[state].enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
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

    /*[ClientRpc]
    public void RpcShipMove(Vector3 position, Vector3 rotate)
    {
        ship.transform.position = position;
        ship.transform.Rotate(rotate);
    }

    [Command]
    public void CmdShipMove(Vector3 transl, Vector3 rotate)
    {
        ship.transform.position =  ship.transform.position + transl;
        ship.transform.Rotate(rotate);

        RpcShipMove(ship.transform.position, rotate);
    }*/
    public void shipMove(Vector3 transl, Vector3 rotate)
    {
        ship.transform.Translate(transl);
        ship.transform.Rotate(rotate);
    }
    [Command]
    public void CmdFire()
    {
        var bullet = (GameObject)Instantiate(bulletPref, ship.transform.position, ship.transform.rotation);
        NetworkServer.SpawnWithClientAuthority(bullet,gameObject);
        Destroy(bullet, 4.0f);
    }
}
