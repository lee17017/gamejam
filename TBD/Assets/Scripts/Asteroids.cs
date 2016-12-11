using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Asteroidensteuerung
public class Asteroids : NetworkBehaviour
{
    float speed = 10;
    public ParticleSystem Explosion;
    public ParticleSystem Explosion2;

	void Start ()
    {
        //Spawneinstellungen
        Transform spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
        transform.LookAt(spaceship);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Bewegung in gerader Richtung
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
	}

    //Zerstörung des Schiffs, falls Spieler "Server" ist
    private void OnTriggerEnter(Collider other)
    {
        if(!isServer)
        {
            return;
        }

        if(other.tag == "bullet")
        {
            Destroy(gameObject);
        }
        else if(other.tag == "shield")
        {
            Instantiate(Explosion2, gameObject.transform);
            Destroy(gameObject);
        }
        else if(other.tag == "SpaceShip")
        {
            other.GetComponentInParent<SpaceShip>().player.takeDamage(5);
            Instantiate(Explosion, gameObject.transform);
            Destroy(gameObject);

        }
        
    }
}
