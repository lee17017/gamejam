using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Asteroids : NetworkBehaviour
{

    private float speed;
    public float minSpeed;
    public float maxSpeed;
	// Use this for initialization
	void Start ()
    {
        Transform spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
        transform.LookAt(spaceship);
        speed = Random.Range(minSpeed, maxSpeed);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
	}
}
