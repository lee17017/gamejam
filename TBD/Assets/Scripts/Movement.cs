using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : Action
{

    private float speed = 10.0f;
    private float rotSpeed = 100.0f;



    public override void Move()
    {
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        //player.CmdShipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
        player.shipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
        CmdRota(player.ship.transform.rotation.eulerAngles.y);
        CmdSyncPosition(player.ship.transform.position);
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