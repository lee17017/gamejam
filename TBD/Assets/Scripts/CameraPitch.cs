using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPitch : MonoBehaviour {

    float pitch;
    float mouseSensitivity = 5f;
    float targetRotation = 0;
    float range = 45;
    float defaultpos = 75;
    public GameObject ship;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        //get value
        pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;
        //set target
        targetRotation -=  pitch;
        targetRotation = Mathf.Clamp(targetRotation, (defaultpos - range), (defaultpos + range));


        //rotate
        transform.Rotate(-pitch, 0, 0);
        ship.GetComponent<SpaceShip>().player.CmdCamUpdate(Quaternion.Euler(
            transform.rotation.x, targetRotation, 90f)); 
    }
}
