using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowShield : MonoBehaviour {

    public float distanceAway;
    public float distanceUp;
    public Transform followedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = followedObject.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
        transform.LookAt(followedObject.position + Vector3.up * distanceUp);	
	}
}
