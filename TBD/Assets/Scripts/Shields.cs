using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shields : Action {

    private float viewRotation;

    public override void Move()
    {
        if(Input.GetAxis("Mouse X") != 0)
        {
            float deltaRot = player.ship.shieldRotSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            viewRotation += deltaRot;
        }

        player.ship.GetComponentInChildren<CameraFollowShield>().transform.rotation = Quaternion.AngleAxis(viewRotation, Vector3.up);

        if(!player.energyDown)
        {
            if(Input.GetMouseButtonDown(0) && !player.ship.shieldActivated)
            {
                StartCoroutine(activateShield(viewRotation - 90));
                player.useEnergy(5f);
            }
        }

    }

    public IEnumerator activateShield(float angle)
    {
            CmdTurnOnShield(angle);
            yield return new WaitForSeconds(3f);
            CmdTurnOffShield();
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
        player.ship.shieldActivated = true;
        player.ship.shieldRotation = degree;
    }
}
