using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareExplosion : MonoBehaviour {

    public GameObject[] flares;

	// Use this for initialization
	void Start () {
        for (int i=0;i<15;i++)
        {
            Instantiate(flares[Random.Range(0,3)],transform.position,Quaternion.identity);
        }
	}
}
