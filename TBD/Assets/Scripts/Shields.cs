using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shields : Action {

    public override void Move()
    {
        float deltaRot = player.ship.shieldRotSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        CmdSetShieldRotation(player.ship.shielRotation + deltaRot);

        if(Input.GetButtonDown("Space") && player.ship.shieldActivated)
        {
            CmdTurnOffShield();            
        }
        if(Input.GetMouseButtonDown(1) && !player.ship.shieldActivated)
        {
            Vector3 mPos = Input.mousePosition;

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
        player.ship.setShieldRotation(degree);
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
        player.ship.shieldActivated.turnOnShield();
        player.ship.setShieldRotation(degree);
    }
}
