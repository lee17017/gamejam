using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : Action
{

    private float speed = 10.0f;
    private float rotSpeed = 100.0f;
    private Vector3 posBefore = Vector3.zero;


    public override void Move()
    {
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        //player.CmdShipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
        player.shipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
        if (Vector3.Distance(posBefore,player.ship.transform.position)>0.05f)
        {
            CmdSyncPosition(player.ship.transform.position);
            posBefore = player.ship.transform.position;
        }
        CmdRota(player.ship.transform.rotation.eulerAngles.y);
    }

    [Command]
    public void CmdSyncPosition(Vector3 pos)
    {
        player.ship.transform.position = pos;
        RpcSyncPosition(pos);
    }

    [ClientRpc]
    public void RpcSyncPosition(Vector3 pos)
    {
        player.ship.transform.position = pos;
    }

    [Command]
    public void CmdRota(float rotY)
    {
        player.ship.transform.rotation = Quaternion.Euler(0,rotY,0);
        RpcSyncRota(rotY);
    }

    [ClientRpc]
    public void RpcSyncRota(float rotY)
    {
        player.ship.transform.rotation = Quaternion.Euler(0, rotY, 0);
    }
}