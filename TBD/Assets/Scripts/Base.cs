using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    float yaw;
    float mouseSensitivity = 5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //get value
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity;

        //rotate
        transform.Rotate(0,0,yaw);
    }
}
