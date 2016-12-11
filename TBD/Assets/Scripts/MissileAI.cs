using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAI : MonoBehaviour {

    //variables
    public float speed;
    public GameObject target;
    public float damage;
    public float health = 10;
    public ParticleSystem Explosion;
    public ParticleSystem Explosion2;
    // Use this for initialization
    void Start () {

        target = GameObject.FindGameObjectWithTag("SpaceShip");
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target.transform);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
	}
    void OnTriggerEnter(Collider c) {
        if (c.tag == "shield")
        {
            health -= 10f;
        }
        else if (c.tag == "SpaceShip")
        {
            //subtract life from player
            Debug.Log("hit");
            c.GetComponentInParent<SpaceShip>().player.takeDamage(30);
            health -= 10;
            
        }
        else if (c.tag == "bullet") {
            health = -5f;
        }
        if (health <= 0f) {
            float tmp = Random.Range(1, 6);
            if (tmp < 4)
            {
                Instantiate(Explosion, gameObject.transform);
            }
            else
            {
                Instantiate(Explosion2, gameObject.transform);
            }
            DestroyObject(this.gameObject); 
        }
    }
}
