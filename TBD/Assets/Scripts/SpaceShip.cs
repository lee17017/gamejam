using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {
    
    public Player player;
    public Camera[] cams;
    public Transform shieldRota;
    public GameObject shield;
    public float coolDown = 2.0f;
    public float timer=0;
    //Shield Control variables
    public bool shieldActivated;
    public float shieldRotSpeed;
    public float shieldRotation;
    public Transform base001, barrel, cam;

    void Update()
    {
        shieldRota.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + shieldRotation + 90, 0);
        shield.active = shieldActivated;
        if (timer > 0)
            timer -= Time.deltaTime;

        shield.GetComponent<Collider>().enabled = shieldActivated;
    }
   
}
