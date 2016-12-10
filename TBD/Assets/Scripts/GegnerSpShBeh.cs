using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GegnerSpShBeh : NetworkBehaviour {

	// Use this for initialization
    int speed;
    int health;
	void Start () {
        //Spawneinstellungen
        Transform spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
        transform.LookAt(spaceship);
	}
	
	// Update is called once per frame
	void Update () {
        //if(genug abstand)
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        //else{//fly in circles around the player and shoot!
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (!isServer)
        {
            return;
        }

        if (other.tag == "bullet")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "SpaceShip")
        {

        }
    }
}
