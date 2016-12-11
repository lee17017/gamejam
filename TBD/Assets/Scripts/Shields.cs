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
        }


        if(player.ship.shieldActivated)
        {
            float deltaRot = player.ship.shieldRotSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            CmdSetShieldRotation(player.ship.shieldRotation + deltaRot);

            if (Input.GetMouseButtonDown(1))
            {
                CmdTurnOffShield();
            }

            //Energy cost
            player.useEnergy(1f * Time.deltaTime);
        }
        else
        {
            if (Input.GetMouseButtonDown(1) && player.energy > 5f)
            {
                Vector3 mPos = Input.mousePosition;
                float width = Screen.width;
                float height = Screen.height;
                if(mPos.x > 0 && mPos.x < width && mPos.y > 0 && mPos.y < height)
                {
                    CmdTurnOnShield(Mathf.Atan2(mPos.y - height / 2, mPos.x - width / 2) * -180 / Mathf.PI);
                }

                //Energy cost
                player.useEnergy(5f);
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
