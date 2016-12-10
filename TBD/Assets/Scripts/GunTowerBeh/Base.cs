using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
    float yaw;
    float mouseSensitivity = 5f;
    float targetRot;
    public float defaultX = -90f;
    public float range;

    // Use this for initialization
    void Start()
    {
        targetRot = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        //get value
        yaw = Input.GetAxis("Mouse X") * mouseSensitivity;

        //combine
        targetRot += yaw;

        //limit angle
        targetRot = Mathf.Clamp(targetRot, -range, range);

        //rotate
        //transform.Rotate(0,0,yaw);
        transform.localRotation = Quaternion.Euler(defaultX, 0f, targetRot);
    }
}
