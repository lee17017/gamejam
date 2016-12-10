using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shields : Action {

    public override void Move()
    {
        if(player.ship.shieldActivated)
        {
            float deltaRot = player.ship.shieldRotSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            CmdSetShieldRotation(player.ship.shieldRotation + deltaRot);

            if (Input.GetKeyDown(KeyCode.X))
            {
                CmdTurnOffShield();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 mPos = Input.mousePosition;
                if(mPos.x > 0 && mPos.x < Screen.width && mPos.y > 0 && mPos.y < Screen.height)
                {

                }
            }
        }
    }

    [Command]
    public void CmdSetShieldRotation(float degree)
    {
        ((Shields)player.actions[2]).RpcSetShieldRotation(degree);
    }

    [ClientRpc]
    public void RpcSetShieldRotation(float degree)
    {
        player.ship.shieldRotation = degree;
    }

    [Command]
    public void CmdTurnOffShield()
    {
        ((Shields)player.actions[2]).RpcTurnOffShield();
    }

    [ClientRpc]
    public void RpcTurnOffShield()
    {
        player.ship.shieldActivated = false;
    }

    [Command]
    public void CmdTurnOnShield(float degree)
    {
        ((Shields)player.actions[2]).RpcTurnOnShield(degree);
    }

    [ClientRpc]
    public void RpcTurnOnShield(float degree)
    {
        StartCoroutine(player.ship.turnOnShield());
        player.ship.shieldRotation = degree;
    }
}
