using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {
    
    public int numberOfPlayers;
    private bool waiting;
    public Player player;
    public Action[] actions;
    public Camera[] cams;

    //Shield Control variables
    public bool shieldActivated;
    public float shieldRotSpeed;
    public float shieldRotation;



    public IEnumerator turnOnShield()
    {
        yield return new WaitForSeconds(1f);
        shieldActivated = true;
    }
}
