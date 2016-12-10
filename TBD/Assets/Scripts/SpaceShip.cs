using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {
    
    public Player player;
    public Camera[] cams;
    public Transform shieldRota;
    public GameObject shield;

    //Shield Control variables
    public bool shieldActivated;
    public float shieldRotSpeed;
    public float shieldRotation;

    void Update()
    {
        shieldRota.rotation = Quaternion.Euler(0,shieldRotation,0);
        shield.active = shieldActivated;
    }

    public IEnumerator turnOnShield()
    {
        yield return new WaitForSeconds(1f);
        shieldActivated = true;
    }
}
