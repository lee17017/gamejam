using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Asteroidensteuerung
public class Asteroids : NetworkBehaviour
{
    float speed = 10;
    public GameObject Explosion;
    public GameObject Explosion2;

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
    public void OnTriggerEnter(Collider other)
    {

        if (other.tag == "bullet")
        {
            Instantiate(Explosion2, transform.position, Quaternion.identity);
        }
        else if (other.tag == "shield")
        {
            Instantiate(Explosion2, transform.position, Quaternion.identity);
        }
        else if (other.tag == "SpaceShip")
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);

        }
        if (!isServer)
        {
            return;
        }

        if(other.tag == "bullet")
        {
            Instantiate(Explosion2, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(other.tag == "shield")
        {
            Instantiate(Explosion2, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(other.tag == "SpaceShip")
        {
            other.GetComponentInParent<SpaceShip>().player.takeDamage(5);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
        
    }
}
