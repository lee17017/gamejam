using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : Action
{
	private bool altMoveBehaviour = false;

	private float speed = 10f;

	// only used for alternative move behaviour
	private Vector3 velocity = new Vector3(0, 0, 0);
	private float SPEED_CAP = 80f;
	private float ACCELERATION = 15f;

    private float rotSpeed = 100.0f;
    private Vector3 posBefore = Vector3.zero;
    private float rotBefore = 0;


    public override void Move()
    {
		if (!altMoveBehaviour) {
			float move = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
			float rot = Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;
			//player.CmdShipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
			player.shipMove (new Vector3 (0, 0, move), new Vector3 (0, rot, 0));
			if (Vector3.Distance (posBefore, player.ship.transform.position) > 0.2f) {
				CmdSyncPosition (player.ship.transform.position);
				posBefore = player.ship.transform.position;
			}
			float y = player.ship.transform.rotation.eulerAngles.y;
			if (Mathf.Abs (rotBefore - y) > 0.5f) {
				CmdRota (y);
				rotBefore = y;
			}
		} else {
			float acceleration = 0;
			if (Input.GetKey (KeyCode.W)) {
				acceleration = ACCELERATION;
			} else if (Input.GetKey (KeyCode.S)) {
				acceleration = -ACCELERATION;
			}
			// No friction in space...
			//      velocity = 0.8f * velocity;
			float rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
			// Rotate velocity which gets added
			Vector3 vec = Quaternion.AngleAxis (rot, Vector3.up) * new Vector3(0, 0, acceleration * Time.deltaTime);
			velocity += vec;
			// Rotate total velocity because of the way shipMove works
			velocity = Quaternion.AngleAxis (-rot, Vector3.up) * velocity;

			// Cap velocity
			var magnitude = velocity.magnitude;
			if (magnitude > SPEED_CAP) {
				velocity = velocity.normalized * SPEED_CAP;
			}

			// Brake
			if (Input.GetKey (KeyCode.Q)) {
				velocity *= 0.95f;
			}

			//player.CmdShipMove(new Vector3(0, 0, move), new Vector3(0, rot, 0));
			player.shipMove(velocity * Time.deltaTime, new Vector3(0, rot, 0));
			if (Vector3.Distance(posBefore,player.ship.transform.position)>0.2f)
			{
				CmdSyncPosition(player.ship.transform.position);
				posBefore = player.ship.transform.position;
			}
			float y = player.ship.transform.rotation.eulerAngles.y;
			if (Mathf.Abs(rotBefore-y)>0.5f)
			{
				CmdRota(y);
				rotBefore = y;
			}
		}
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
        if (isLocalPlayer) { return; }
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
        if (isLocalPlayer) { return; }
        player.ship.transform.rotation = Quaternion.Euler(0, rotY, 0);
    }
}