using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Asteroids : NetworkBehaviour
{

    float speed = 10;
	// Use this for initialization
	void Start ()
    {
        Transform spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
        transform.LookAt(spaceship);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(!isServer)
        {
            return;
        }

        if(other.tag == "bullet")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "SpaceShip")
        {
            Debug.Log("Ship hit");
        }
    }
}
