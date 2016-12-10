using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    private Vector3 direction;
    public float speed;

	// Use this for initialization
	void Start () {
        direction = Random.onUnitSphere*Random.Range(0.7f,1);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += direction*speed;
	}
}
