using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPitch : MonoBehaviour {

    float pitch;
    float mouseSensitivity = 5f;
    float verticalRot = 0;
    float targetRotation = -125f;
    float defaultPos;//=-125f;
    float rangeLow = -180f;
    float rangeHi =  - 90f;

    // Use this for initialization
    void Start () {
        defaultPos = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {
        //get the value
        pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //combine
        targetRotation -= pitch;

        targetRotation = Mathf.Clamp(targetRotation, rangeLow, rangeHi);
        
        //rotate
        //transform.Rotate(0, -pitch, 0);
        transform.localRotation = Quaternion.Euler(transform.rotation.x, targetRotation, -90f);



	}
}
