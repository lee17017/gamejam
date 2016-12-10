using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : Action {

    public override void Move()
    {
        if (Input.GetButton("Fire1") && player.ship.timer<=0)
        {
            player.ship.timer = player.ship.coolDown;
            player.CmdFire();
        }
    }
}
