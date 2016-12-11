using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowShield : MonoBehaviour {
    
    public float distanceUp;
    public Transform followedObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = followedObject.position + Vector3.up * distanceUp;
	}
}
