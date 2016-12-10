using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
       
    }

}