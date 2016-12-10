using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour{
    public bool friendly = true;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().velocity = transform.forward * 20;
	}

    void OnTriggerEnter(Collider other)
    {
        if (!friendly)
        {
            Debug.Log(other.tag);
            if (!isServer)
            {
                return;
            }

            if (other.tag == "shield")
            {
                Destroy(gameObject);
            }
            else if (other.tag == "SpaceShip")
            {
                other.GetComponentInParent<SpaceShip>().player.takeDamage(30);
                Destroy(gameObject);
            }
        }
        
    }
}
