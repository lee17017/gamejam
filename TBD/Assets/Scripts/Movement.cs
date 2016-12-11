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
        if (player.energyDown)
            return;
        float move = 0;
        float rot = 0;
		if (!altMoveBehaviour) {
			if (Input.GetAxis ("Vertical") != 0) {
				var pss = player.ship.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem ps in pss) {
					var emission = ps.emission;
					emission.enabled = true;
				}
			} else {
				var pss = player.ship.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem ps in pss) {
					var emission = ps.emission;
					emission.enabled = false;
				}
			}
			move = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
			rot = Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;
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

				var pss = player.ship.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem ps in pss) {
					var emission = ps.emission;
					emission.enabled = true;
				}
			} else if (Input.GetKey (KeyCode.S)) {
				acceleration = -ACCELERATION;

				var pss = player.ship.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem ps in pss) {
					var emission = ps.emission;
					emission.enabled = false;
				}
			} else {
				var pss = player.ship.GetComponentsInChildren<ParticleSystem> ();
				foreach (ParticleSystem ps in pss) {
					var emission = ps.emission;
					emission.enabled = false;
				}
			}
			// No friction in space...
			//      velocity = 0.8f * velocity;
			rot = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
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

            // We don't use move but we set it not 0 so energy gets used up
			if (velocity.magnitude > 0) {
				move = 1;
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

        //Energy cost
        if(Mathf.Abs(move) + Mathf.Abs(rot) > 0)
        {
            player.useEnergy(1f * Time.deltaTime);
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