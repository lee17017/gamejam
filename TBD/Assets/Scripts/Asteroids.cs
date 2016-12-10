using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Asteroids : NetworkBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Transform spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
        transform.LookAt(spaceship);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + Vector3.forward * Time.deltaTime;
	}
}
