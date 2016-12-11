using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : Action {
    GameObject barrel, base001;
    //barrel
    float pitch;
    float mouseSensitivity = 5f;
    float verticalRot = 0;
    float targetRotation = -125f;
    float defaultPos;//=-125f;
    float rangeLow = -180f;
    float rangeHi = -90f;
    //base
    float yaw;
    float targetRot=0;
    public float defaultX = -90f;
    public float range;

    public override void Move()
    {
        if (barrel == null && base001 == null)
        { 
            barrel = GameObject.Find("Barrel");
            base001 = GameObject.Find("Base001");
            defaultPos = barrel.transform.rotation.y;
        }

        // Barrel Part:

        //get the value
        pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //combine
        targetRotation -= pitch;

        targetRotation = Mathf.Clamp(targetRotation, rangeLow, rangeHi);

        //rotate
        //transform.Rotate(0, -pitch, 0);
        barrel.transform.localRotation = Quaternion.Euler(transform.rotation.x, targetRotation, -90f);



        // Base Part:
        
        //get value
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity;

        //combine
        targetRot += yaw;

        //limit angle
        targetRot = Mathf.Clamp(targetRot, -range, range);

        //rotate
        //transform.Rotate(0,0,yaw);
        base001.transform.localRotation = Quaternion.Euler(defaultX, 0f, targetRot);


        CmdSyncBarrel(barrel.transform.localRotation);
        CmdSyncBase(base001.transform.localRotation);
        if (Input.GetButton("Fire1") && player.ship.timer<=0 && !player.energyDown)
        {
            player.ship.timer = player.ship.coolDown;
            player.CmdFire();

            //Energy cost
            player.useEnergy(1f);
        }



    }


    [Command]
    public void CmdSyncBarrel(Quaternion rot)
    {
        player.ship.barrel.localRotation = rot;
        RpcSyncBarrel(rot);
    }

    [ClientRpc]
    public void RpcSyncBarrel(Quaternion rot)
    {
        if (isLocalPlayer) { return; }
        player.ship.barrel.localRotation = rot;
    }

    [Command]
    public void CmdSyncBase(Quaternion rot)
    {
        player.ship.base001.localRotation = rot;
        RpcSyncBase(rot);
    }

    [ClientRpc]
    public void RpcSyncBase(Quaternion rot)
    {
        if (isLocalPlayer) { return; }
        player.ship.base001.localRotation = rot;
    }

}
