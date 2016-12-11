using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GegnerSpShBeh : NetworkBehaviour {

	// Use this for initialization
    int speed;
    int health;
    public float radius;
    bool attacking=false;
    GameObject Projectile;
    public ParticleSystem Explosion;
    public ParticleSystem Explosion2;
    private Transform spaceship;

	void Start () {
        //Projectile
        spaceship = GameObject.Find("SpaceShip").GetComponent<SpaceShip>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(spaceship);

        if(!attacking)
        {
            if (Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z), new Vector2(spaceship.transform.position.x, spaceship.transform.position.z)) <= radius)
            {
                attacking = true;
                StartCoroutine(Attack());
            }
            else
            {
                transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            }
        }

	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (!isServer)
        {
            return;
        }

        if (other.tag == "bullet"&&other.GetComponent<Bullet>().friendly)
        {
            Instantiate(Explosion, gameObject.transform);
            Destroy(gameObject);
        }
        else if (other.tag == "SpaceShip")
        {

            other.GetComponentInParent<SpaceShip>().player.takeDamage(10);
        }
        else if (other.tag == "shield")
        {
            Instantiate(Explosion2, gameObject.transform);
            Destroy(gameObject);
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject tmp= (GameObject)Instantiate(Projectile, gameObject.transform);
        tmp.GetComponent<Bullet>().friendly = false;
        //Evtl Positionsanpassung nötig
        yield return new WaitForSeconds(1f);
        tmp = (GameObject)Instantiate(Projectile, gameObject.transform);
        tmp.GetComponent<Bullet>().friendly = false;

        yield return new WaitForSeconds(1f);
        tmp = (GameObject)Instantiate(Projectile, gameObject.transform);
        tmp.GetComponent<Bullet>().friendly = false;

        yield return new WaitForSeconds(1f);
        tmp = (GameObject)Instantiate(Projectile, gameObject.transform);
        tmp.GetComponent<Bullet>().friendly = false;

        yield return new WaitForSeconds(1f);
        tmp = (GameObject)Instantiate(Projectile, gameObject.transform);
        tmp.GetComponent<Bullet>().friendly = false;
        attacking = false;
    }
}
